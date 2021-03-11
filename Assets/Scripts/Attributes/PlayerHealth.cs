using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerStats playerStats;

    private int currentHealth;
    private PlayerAttributes maxHealthAttr;
    private Rigidbody2D rb;
    private PlayerMovement movement;

    private float knockback  = 500;

    private bool invincible = false;
    private float invincibilityDeltaTime = 0.15f;
    private float invincibilityDuration = 1.5f;

    private SpriteRenderer sr;
    [SerializeField]
    private Color flashColor;
    [SerializeField]
    private Color regularColor;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
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
        if (!invincible)
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
                //TODO: Switching directions when getting hit gives a boost
                StartCoroutine(movement.LoseControl(0.25f));
                StartCoroutine(TemporaryInvincibility());
                // Add knockback to player
                Vector2 side = transform.position - source.position;
                if (side.x > 0)
                {
                    // source on the right side
                    rb.AddForce(new Vector2(knockback, knockback));
                }
                else
                {
                    rb.AddForce(new Vector2(-knockback, knockback));
                }
            }
        }
    }

    private void Die()
    {
        // Play death animation
        Debug.Log("ded");
    }

    IEnumerator TemporaryInvincibility()
    {
        invincible = true;

        for (float i = 0; i < invincibilityDuration; i += invincibilityDeltaTime)
        {
            if (sr.color == regularColor)
            {
                sr.color = flashColor;
                //transform.localScale = Vector3.zero;
            }
            else
            {
                sr.color = regularColor;
                //transform.localScale = Vector3.one;
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        sr.color = regularColor;
        invincible = false;
    }

    
}
