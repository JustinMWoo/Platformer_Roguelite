using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField]
    int collisionDamage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collisionDamage != 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("collision");
                collision.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(transform, collisionDamage);
            }
        }
    }
}
