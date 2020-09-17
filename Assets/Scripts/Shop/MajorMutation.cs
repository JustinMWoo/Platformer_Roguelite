using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Major Mutation")]
public class MajorMutation : ScriptableObject
{
    public GameObject MutationPrefab;
    public Sprite mutationSprite;
    public string mutationDescription;
    public int cost;
}
