using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttributes
{
    public Attributes attribute;
    public float amount;

    public PlayerAttributes(Attributes attribute, float amount)
    {
        this.attribute = attribute;
        this.amount = amount;
    }
}
