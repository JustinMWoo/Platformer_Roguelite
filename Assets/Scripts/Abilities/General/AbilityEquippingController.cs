using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class AbilityEquippingController : MonoBehaviour
{
    private static AbilityEquippingController _current;
    public static AbilityEquippingController Current { get { return _current; } }


    public GameObject player;
    public PlayerStats playerStats;

    public AbilityCooldown[] abilitySlots;
    public GameObject abilityDropPrefab;

    private bool touchingAbilityDrop = false;
    private List<GameObject> abilityDrops = new List<GameObject>();

    public AbilityHolder droppableAbilities;


    // Singleton
    private void Awake()
    {
        if (_current != null && _current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _current = this;
        }
    }

    void Update()
    {
        if (touchingAbilityDrop)
        {
            GameObject abilityDrop = GetClosestAbility();
            abilityDrop.GetComponent<AbilityItem>().DisplayAbility();
            foreach (GameObject ability in abilityDrops)
            {
                if (ability != abilityDrop)
                {
                    ability.GetComponent<AbilityItem>().HideAbility();
                }
            }
        }
    }

    public void Equip(AbilityCooldown abilitySlot)
    {
        GameObject closestAbility = GetClosestAbility();
        Ability ability = closestAbility.GetComponent<AbilityItem>().ability;

        if ((ability is MovementAbility) == abilitySlot.movementAbility)
        {
            if (abilitySlot.GetAbility())
                abilitySlot.GetAbility().Unequip();

            foreach (AbilityCooldown abilityCooldown in abilitySlots)
            {
                abilityCooldown.UpdateCooldown();
            }



            abilitySlot.Initialize(ability, player);
            Destroy(closestAbility);
        }
    }

    // Add the ability that has been touched into the list
    public void AddEquippableAbility(GameObject abilityDrop)
    {
        touchingAbilityDrop = true;
        abilityDrops.Add(abilityDrop);
    }

    public void RemoveEquippableAbility(GameObject abilityDrop)
    {
        abilityDrops.Remove(abilityDrop);
        if (abilityDrops.Count < 1)
            touchingAbilityDrop = false;
        abilityDrop.GetComponent<AbilityItem>().HideAbility();
    }


    // Gets the closest ability dropped to the player that is in range
    // Returns null if there are no objects in range
    public GameObject GetClosestAbility()
    {
        GameObject closestAbility = null;
        float minDistance = float.MaxValue;
        foreach (GameObject abilityDrop in abilityDrops)
        {
            float newDistance = Vector2.Distance(player.transform.position, abilityDrop.transform.position);
            if (newDistance < minDistance)
            {
                minDistance = newDistance;
                closestAbility = abilityDrop;
            }
        }
        return closestAbility;
    }

    // Is the player touching an ability
    public bool IsTouchingAbility()
    {
        return touchingAbilityDrop;
    }

    // Drop an item from the player
    public void DropItemOnPlayer(Ability ability)
    {
        GameObject droppedItem = Instantiate(abilityDropPrefab, player.transform.position, Quaternion.identity);

        Ability abilityInstance = Instantiate(ability);

        droppedItem.GetComponent<AbilityItem>().ability = abilityInstance;
    }

    // Drop an item of the specified tier
    public void DropItem(int tier, Vector3 position)
    {
        // Checking for index out of bounds 
        // Only run if the tier of ability being dropped exists
        if (tier <= droppableAbilities.GetNumTiers() - 1)
        {
            Ability[] abilitiesInTier = droppableAbilities.GetAbilitiesInTier(tier);

            GameObject droppedItem = Instantiate(abilityDropPrefab, position, Quaternion.identity);

            droppedItem.GetComponent<AbilityItem>().ability = abilitiesInTier[UnityEngine.Random.Range(0, abilitiesInTier.Length)];
        }
    }

    public int[] GetDropTableForLevel(int level)
    {
        return droppableAbilities.GetDropTableForLevel(level);
    }
}
