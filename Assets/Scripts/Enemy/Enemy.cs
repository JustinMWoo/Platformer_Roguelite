using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int numTiers;

    public int currentHealth;

    // TODO: Maybe make a different boss class and each boss is a subclass of it because bosses are going to be very different
    [SerializeField]
    bool isBoss = false;
    [SerializeField]
    int maxHealth = 0;
    [SerializeField]
    int level;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // Not sure if this is necessary
        numTiers = AbilityController.Current.droppableAbilities.GetNumTiers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if (isBoss)
            GameManager.Current.bossAlive = false;
        RollForItem();
        Destroy(gameObject);
    }

    void RollForItem()
    {
        // Get the drop table for the units level
        int[] dropTable = AbilityController.Current.GetDropTableForLevel(level);

        // Roll for an item drop
        int roll = Random.Range(0, 100);
        Debug.Log("Roll: " + roll);

        // Check if the tier of item is dropped, if not increment the tier and check again
        int tier = 0;
        foreach (int chance in dropTable)
        {
            if (roll <= chance)
            {
                AbilityController.Current.DropItem(tier, transform.position);
                return;
            }
            else
            {
                roll -= chance;
            }
            tier++;
        }
    }

    #region Damage Over Time

    public void DamageOverTime(int damage)
    {

    }

    #endregion
}
