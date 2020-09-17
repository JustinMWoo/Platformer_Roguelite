using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAttributes 
{
    public float Value
    {
        get
        {
            if (isDirty || baseValue != lastBaseValue)
            {
                lastBaseValue = baseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    public Attributes attribute;

    List<StatMod> statMods;
    public float baseValue;
    bool isDirty = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;

    public PlayerAttributes()
    {
        statMods = new List<StatMod>();
    }

    public PlayerAttributes(Attributes attribute, float value): this()
    {
        this.attribute = attribute;
        baseValue = value;
    }

    public void AddMod(StatMod mutation)
    {
        isDirty = true;
        statMods.Add(mutation);
        statMods.Sort(CompareMutationOrder);
    }

    public bool RemoveMod(StatMod mutation)
    {
        if (statMods.Remove(mutation))
        {
            isDirty = true;
            return true;
        }

        else
            return false;
    }

    public virtual bool RemoveAllMutationsFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statMods.Count - 1; i >=0; i--)
        {
            if(statMods[i].source == source)
            {
                isDirty = true;
                statMods.RemoveAt(i);
                didRemove = true;
            }
        }
        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;   // final value starts as atleast the base value
        float sumPercentAdd = 0;

        // adds each statModifier value to the final value (initially just the base value)
        // handles percentage modifiers differently than flat modifiers
        for (int i = 0; i < statMods.Count; i++)
        {
            StatMod mod = statMods[i];

            if (mod.type == StatModType.Flat)
            {
                finalValue += mod.value;
            }

            else if (mod.type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.value;     // add percent to value

                if (i + 1 >= statMods.Count || statMods[i + 1].type != StatModType.PercentAdd)    // if the next mod in list is not of type percentAdd or we reach the end of the list
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }

            else if (mod.type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.value; // percent values are represented as decimals.
            }
        }

        // round the final value to 4 decimal places
        return (float)Math.Round(finalValue, 4);
    }

    public void ResetMutations()
    {
        statMods.Clear();
        isDirty = true;
    }

    protected virtual int CompareMutationOrder(StatMod a, StatMod b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order)
            return 1;
        return 0;
    }
}
