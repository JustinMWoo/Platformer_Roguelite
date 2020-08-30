using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

[CreateAssetMenu (menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{
    public int projectileSpeed = 1;
    public GameObject projectilePrefab;
    public string projectileSpawn;

    //public int numProjectiles;
    //public int projectileSpread;

    private FireProjectileTriggerable fireProj;

    public override void Initialize(GameObject player) 
    {
        base.Initialize(player);
        fireProj = player.GetComponent<FireProjectileTriggerable>();
        fireProj.projectileSpeed = projectileSpeed;
        fireProj.projectilePrefab = projectilePrefab;
        fireProj.projectileSpawn = projectileSpawn;
    }

    public override void TriggerAbility()
    {
        fireProj.FireProjectile();
    }

    public override void Unequip()
    {
        throw new System.NotImplementedException();
    }
}
