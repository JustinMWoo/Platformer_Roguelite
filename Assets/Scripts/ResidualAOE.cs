using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidualAOE : MonoBehaviour
{
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public float duration;
    [HideInInspector]
    public float tickTime = 1f;
    private Dictionary<Collider2D, float> colliderDict = new Dictionary<Collider2D, float>();

    private ParticleSystem AOEParticles;

    private void Start()
    {
        AOEParticles = GetComponentInChildren<ParticleSystem>();
        AOEParticles.Stop();
        ParticleSystem.MainModule main = AOEParticles.main;
        main.duration = duration;
        AOEParticles.Play();
    }

    private void Update()
    {
        // Destroy the AOE after the duration
        if (AOEParticles)
        {
            if (!AOEParticles.IsAlive())
            {
                Destroy(gameObject);
            }
        }
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
            Debug.Log("Dealing " + damage + " damage");
            colliderDict[collision] = Time.time + tickTime;
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
