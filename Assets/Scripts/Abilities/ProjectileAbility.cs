using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ProjectileSpawn
{
    Head = 1
}

[CreateAssetMenu (menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{
    [HideInInspector]
    public bool residualAOEField;
    [HideInInspector]
    public int residualAOEDamage = 1;
    [HideInInspector]
    public GameObject residualAOEPrefab;
    [HideInInspector]
    public float residualAOEDuration = 1;

    public float projectileSpeed = 1;
    public GameObject projectilePrefab;
    public ProjectileSpawn projectileSpawn;
    public int projectileDamage = 1;
    public int projectileNumber = 1;
    public float spread = 0;
   
    private FireProjectileTriggerable fireProj;

    public override void Initialize(GameObject player) 
    {
        // Initialize the ability with the stats in the scriptable object
        base.Initialize(player);
        fireProj = player.AddComponent<FireProjectileTriggerable>();
        
        fireProj.projectileSpeed = projectileSpeed;
        fireProj.projectilePrefab = projectilePrefab;
        fireProj.projectileSpawn = projectileSpawn;
        fireProj.projectileDamage = projectileDamage;
        fireProj.projectileNumber = projectileNumber;
        fireProj.spread = spread;

        // If the projectile leaves behind an AOE field
        if (residualAOEField)
        {
            fireProj.residualAOEDamage = residualAOEDamage;
            fireProj.residualAOEDuration = residualAOEDuration;
            fireProj.residualAOEPrefab = residualAOEPrefab;
        }
    }

    // Fire the projectile
    public override void TriggerAbility()
    {
        fireProj.FireProjectile();
    }

    public override void Unequip()
    {
        base.Unequip();
        // Uses AbilityEquippingController
        // Removes the FireProjectileTriggerable script from the player
        // removes the mutation from the player (Do this in the parent class and call base to have this do it too)

        Destroy(fireProj);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(ProjectileAbility))]
public class ProjectileAbility_Editor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ProjectileAbility script = (ProjectileAbility)target;

        script.residualAOEField = EditorGUILayout.Toggle("AOE Field", script.residualAOEField);
        if (script.residualAOEField)
        {
            script.residualAOEDamage = EditorGUILayout.IntField("Residual AOE Damage", script.residualAOEDamage);
            script.residualAOEDuration = EditorGUILayout.FloatField("Residual AOE Duration", script.residualAOEDuration);
            script.residualAOEPrefab = EditorGUILayout.ObjectField("Residual AOE Prefab", script.residualAOEPrefab, typeof(GameObject), true) as GameObject;
        }

    }
}
#endif
