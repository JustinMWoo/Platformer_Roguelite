using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    //protected PlayerStats stats;

    public string aName = "New Ability";
    public string aDescription = "Description";
    public Sprite aSprite;
    public float aBaseCooldown = 1f;

    public Mutation mutations;

    public bool movement = false;

    private PlayerStats stats;

    public virtual void Initialize(GameObject player) 
    {
        stats = player.GetComponent<PlayerStats>();

        if (mutations!=null)
        {
            foreach (StatMod mutation in mutations.statMods)
            {
                PlayerAttributes attr = stats.Find(mutation.affectedAttribute.name);
                if (attr != null)
                {
                    attr.AddMod(mutation);
                }
            }
           

            //// Enumerate through the attributes affected by the mutation on the skill
            //List<PlayerAttributes>.Enumerator attributes = mutation.affectedAttributes.GetEnumerator();
            //while (attributes.MoveNext())
            //{
            //    Debug.Log(attributes.Current.attribute);

            //    PlayerAttributes attr = stats.Find(attributes.Current.attribute.name.ToString());
            //    if (attr !=null)
            //    {
            //        attr.AddMutation(mutation);
            //    }
            //}
        }
    }

    public abstract void TriggerAbility();

    public virtual void Unequip()
    {

        if (mutations!=null)
        {
            foreach (StatMod mutation in mutations.statMods)
            {
                PlayerAttributes attr = stats.Find(mutation.affectedAttribute.name);
                if (attr != null)
                {
                    attr.RemoveMod(mutation);
                }
            }
        }
        AbilityEquippingController.Current.DropItemOnPlayer(this);
    }
   
}
