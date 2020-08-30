using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStats
{
    float baseValue;
    bool isDirty = true;

    readonly List<Mutation> mutations;

    public AbilityStats()
    {
        mutations = new List<Mutation>();
    }

    public AbilityStats(float value) : this()
    {
        baseValue = value;
    }

    public void AddMutation(Mutation mutation)
    {
        isDirty = true;
        mutations.Add(mutation);
    }

    public bool RemoveMutation(Mutation mutation)
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
