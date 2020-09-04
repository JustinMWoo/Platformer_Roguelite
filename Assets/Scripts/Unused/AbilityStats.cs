using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStats
{
    float baseValue;
    bool isDirty = true;
    

    readonly List<StatMod> mutations;

    public AbilityStats()
    {
        mutations = new List<StatMod>();
    }

    public AbilityStats(float value) : this()
    {
        baseValue = value;
    }

    public void AddMutation(StatMod mutation)
    {
        isDirty = true;
        mutations.Add(mutation);
    }

    public bool RemoveMutation(StatMod mutation)
    {
        if (mutations.Remove(mutation))
        {
            isDirty = true;
            return true;
        }

        else 
            return false;
    }




    public void ResetMutations()
    {
        mutations.Clear();
        isDirty = true;
    }
}
