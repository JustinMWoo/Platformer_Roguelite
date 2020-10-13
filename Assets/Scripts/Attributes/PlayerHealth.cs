using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerStats playerStats;

    private int currentHealth;
    private PlayerAttributes maxHealthAttr;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        maxHealthAttr = playerStats.Find("MaxHealth");
        currentHealth = Mathf.RoundToInt(maxHealthAttr.Value);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Heal(int amount)
    {
        if (currentHealth + amount <= maxHealthAttr.Value)
        {
            currentHealth += amount;
        }
        else
        {
            currentHealth = Mathf.RoundToInt(maxHealthAttr.Value);
        }
            
    }

    // Take damage and knockback the player
    public void TakeDamage(Transform source, int damage)
    {
        Debug.Log("Taking damage");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();

            // MAYBE CALL THIS FROM THE END OF THE DEATH ANIMATION!
            GameManager.Current.LoseGame();
        }
        else
        {
            // Add knockback to player
            // Not working properly
            Vector2 force = transform.position - source.position + Vector3.up;
            rb.AddForce(force.normalized * 500f);
        }
    }

    private void Die()
    {
        // Play death animation
        Debug.Log("ded");
    }
}
