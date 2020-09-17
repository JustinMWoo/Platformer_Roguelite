using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathAttackTriggerable : MonoBehaviour
{
    public bool init;
    public PlayerStats stats;
    public GameObject particleSystemPrefab;
    public int damage;

    private GameObject particleSystemObject;
    private ParticleSystem breathParticleSystem;
    private ParticleSystemDamage particleSystemDamage;


    void Start()
    {
        // Get the stats of the player to apply modifiers to the projectile
        stats = GetComponent<PlayerStats>();
    }
    void Initialize()
    {
        init = true;
        particleSystemObject = Instantiate(particleSystemPrefab, transform.Find("Head"));
        breathParticleSystem = particleSystemObject.GetComponent<ParticleSystem>();
        particleSystemDamage = particleSystemObject.GetComponent<ParticleSystemDamage>();
        breathParticleSystem.Stop();
    }

    public void BreathAttack()
    {
        if (!init)
        {
            Initialize();
        }

        // Compute damage here to change the value depending on the players stats at the time
        particleSystemDamage.damage = damage;


        // Change spread of particles
        //breathParticleSystem.shape.randomDirectionAmount();

        breathParticleSystem.Play();
    }

    public void Unequip()
    {
        Destroy(particleSystemObject);
    }

}
