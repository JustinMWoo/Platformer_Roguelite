using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public enum ProjectileSpawn
{
    Head = 1
}

[CreateAssetMenu (menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{
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
