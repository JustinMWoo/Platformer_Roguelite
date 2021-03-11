using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileTriggerable : MonoBehaviour
{
    #region Variables
    // Basic projectile information
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public ProjectileSpawn projectileSpawn;
    public PlayerStats stats;
    public int projectileNumber;
    public int projectileDamage;
    public float spread;
    public Transform spawnLocation;

    // Residual AOE information
    public int residualAOEDamage;
    public GameObject residualAOEPrefab;
    public float residualAOEDuration;

    private bool init = false;

    #endregion

    void Start()
    {
        // Get the stats of the player to apply modifiers to the projectile
        stats = GetComponent<PlayerStats>();
    }

    // Fire the projectile according to the specifications from the scriptable object ProjectileAbility
    public void FireProjectile()
    {
        if (!init && projectileSpawn == ProjectileSpawn.Head)
        {
            spawnLocation = transform.Find("Head");
            init = true;
        }

        for (int i = 0; i < projectileNumber; i++)
        {
            Invoke("CreateProjectile", i * 0.1f);
        }
    }

    // Create a projectile using the prefab provided
    private void CreateProjectile()
    {
        GameObject proj = Instantiate(projectilePrefab, spawnLocation.position, spawnLocation.rotation);
        Projectile newProj = proj.GetComponent<Projectile>();

        // Add spread to the projectile
        proj.transform.Rotate(0, 0, Random.Range(-spread, spread));

        // Set the properties of the projectile
        newProj.SetSpeed(stats.Find("ProjectileSpeed").Value * projectileSpeed);
        newProj.direction = newProj.transform.right;
        newProj.playerProjectile = true;
        newProj.damage = projectileDamage;

        if (residualAOEPrefab != null)
        {
            // If the projectile leaves behind an AOE field
            newProj.residualAOEPrefab = residualAOEPrefab;
            newProj.residualAOEDamage = residualAOEDamage;
            newProj.residualAOEDuration = residualAOEDuration;
        }
    }
}
