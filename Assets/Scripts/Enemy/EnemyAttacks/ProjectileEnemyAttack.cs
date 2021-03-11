using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyAttack : EnemyAttack
{
    private EnemyAI ai;
    private Transform target;
    private bool aiming;

    [SerializeField]
    private GameObject projectilePrefab; // Prefab for projectile
    [SerializeField]
    private int damage;
    [SerializeField]
    private float projSpeed;

    private void Start()
    {
        ai = GetComponent<EnemyAI>();
    }

    public override void Execute()
    {
        if (!aiming)
        {
            target = ai.GetTarget();
            StartCoroutine(FireProjectile());
        }
    }

    IEnumerator FireProjectile()
    {
        aiming = true;
        yield return new WaitForSeconds(3);
        // If still in range then spawn projectile
        if (ai.GetAttackStatus())
        {
            GameObject projObj = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Projectile proj = projObj.GetComponent<Projectile>();
            proj.damage = damage;
            proj.SetSpeed(projSpeed);
            proj.playerProjectile = false;
            proj.direction = (target.transform.position - projObj.transform.position).normalized;
            aiming = false;
        }
    }
}
