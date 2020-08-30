using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mutation")]
public class Mutation : ScriptableObject
{
    public List<PlayerAttributes> affectedAttributes = new List<PlayerAttributes>();
}
