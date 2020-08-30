using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    //protected PlayerStats stats;

    public string aName = "New Ability";
    public Sprite aSprite;
    public float aBaseCooldown = 1f;

    public Mutation mutation;

    public virtual void Initialize(GameObject player) 
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        if (mutation != null)
        {
            // Enumerate through the attributes affected by the mutation on the skill
            List<PlayerAttributes>.Enumerator attributes = mutation.affectedAttributes.GetEnumerator();
            while (attributes.MoveNext())
            {
                PlayerAttributes attr = stats.Find(attributes.Current.attribute.name.ToString());
                if (attr !=null)
                {
                    attr.amount += attributes.Current.amount;
                }
            }
        }
    }

    public abstract void TriggerAbility();

    public abstract void Unequip();
}
