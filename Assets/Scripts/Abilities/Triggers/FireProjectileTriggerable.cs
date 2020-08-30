using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileTriggerable : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public string projectileSpawn;
    public PlayerStats stats;

    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    public void FireProjectile()
    {
        GameObject spine = Instantiate(projectilePrefab, transform.Find(projectileSpawn).position , transform.Find(projectileSpawn).rotation);

        spine.GetComponent<Projectile>().SetSpeed(stats.Find("ProjectileSpeed").amount * projectileSpeed);
    }
}
