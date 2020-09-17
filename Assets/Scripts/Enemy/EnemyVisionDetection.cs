using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionDetection : MonoBehaviour
{
    private bool playerInSight = false;
    private Transform playerTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // If there is a line to the player
            Vector2 dir = collision.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
            if (hit.collider.CompareTag("Player"))
            {
                playerInSight = true;
                playerTransform = collision.transform;
            }
        }
    }

    public Transform PlayerInSight()
    {
        if (playerInSight)
        {
            return playerTransform;
        }
        else
        {
            return null;
        }
    }
}
