using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDamage : MonoBehaviour
{
    [HideInInspector]
    public int damage;

    private ParticleSystem damagingParticleSystem;
    private CharacterController2D charController;
    //private float totalTime = 0;
    private Collider2D damageCollider;

    private float tickTime = 1f;
    private Dictionary<Collider2D, float> colliderDict = new Dictionary<Collider2D, float>();

    private void Start()
    {
        damagingParticleSystem = GetComponent<ParticleSystem>();
        charController = GetComponentInParent<CharacterController2D>();
        damageCollider = GetComponent<Collider2D>();
        charController.AddBreathAttack(damagingParticleSystem);
    }

    private void Update()
    {
        if (damagingParticleSystem.isPlaying)
        {
            damageCollider.enabled = true;
        }
        else
        {
            damageCollider.enabled = false;

            if (colliderDict.Count > 0)
                colliderDict.Clear();
        }
    }

    private void OnDestroy()
    {
        charController.RemoveBreathAttack(damagingParticleSystem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !colliderDict.ContainsKey(collision))
        {
            colliderDict[collision] = float.NegativeInfinity;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        float timer;
        if (!colliderDict.TryGetValue(collision, out timer)) return;

        if (Time.time > timer)
        {
            colliderDict[collision] = Time.time + tickTime;
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }

        //totalTime += Time.deltaTime;
        //if (totalTime > 1 && collision.CompareTag("Enemy"))
        //{
        //    collision.GetComponent<Enemy>().TakeDamage(damage);
        //    totalTime = 0;
        //}
    }
}
