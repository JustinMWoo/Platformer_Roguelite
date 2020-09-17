using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Mutation")]
public class Mutation : ScriptableObject
{
    public StatMod[] statMods;

    [HideInInspector]
    public bool generalMutation;
    [HideInInspector]
    public Sprite mutationSprite;
    [HideInInspector]
    public string mutationDescription;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Mutation))]
public class Mutation_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Mutation script = (Mutation)target;

        script.generalMutation = EditorGUILayout.Toggle("General Mutation", script.generalMutation);
        if (script.generalMutation)
        {
            script.mutationSprite = EditorGUILayout.ObjectField("Residual AOE Prefab", script.mutationSprite, typeof(Sprite), true) as Sprite;
            script.mutationDescription = EditorGUILayout.TextField("Residual AOE Damage", script.mutationDescription);
        }

    }
}
#endif