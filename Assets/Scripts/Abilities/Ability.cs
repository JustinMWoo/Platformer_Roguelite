using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    protected PlayerStats stats;
    public string name;
    public Ability(PlayerStats stats)
    {
        this.stats = stats;
    }

    public virtual void Execute(GameObject player)
    {

    }

    public virtual void Unequip()
    {

    }
}
