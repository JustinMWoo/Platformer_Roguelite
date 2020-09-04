using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Mutation")]
public class Mutation : ScriptableObject
{
    public StatMod[] statMods;
    public Sprite mutationSprite;
    public string mutationDescription;
}


