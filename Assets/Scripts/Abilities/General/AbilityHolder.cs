using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiDimensionalAbility
{
    public Ability[] abilityArray;
}

[System.Serializable]
public class MultiDimensionalInt
{
    public int[] intArray;
}

[CreateAssetMenu(menuName = "Abilities/AbilityHolder")]
public class AbilityHolder : ScriptableObject
{
    [Header("Nested Arrays for Abilities (for each tier)")]
    [SerializeField]
    private MultiDimensionalAbility[] abilities;

    // Enemy tier, ability tier, 
    [Header("Nested Arrays for Drop Table", order = 0), Space(-10, order = 1), Header("(level of enemy, drop chance for each tier of ability)", order = 2)]
    [SerializeField]
    private MultiDimensionalInt[] dropTable;


    public Ability[] GetAbilitiesInTier(int tier)
    {
        return abilities[tier].abilityArray;
    }

    public int GetNumTiers()
    {
        return abilities.Length;
    }

    public int[] GetDropTableForLevel(int level)
    {
        return dropTable[level].intArray;
    }
}
