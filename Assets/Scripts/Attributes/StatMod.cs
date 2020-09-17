using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300
}

[Serializable]
public class StatMod
{
    public Attributes affectedAttribute;

    public float value;
    public StatModType type;
    public int order;
    public Ability source;
    
}

