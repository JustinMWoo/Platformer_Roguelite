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

    List<GameObject> abilityDisplay = new List<GameObject>();
    public PlayerAbilityController player;
    public PlayerStats playerStats;

    public AbilityCooldown[] abilityCooldowns;

    // Singleton
    private void Awake()
    {
        if(_current !=null && _current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _current = this;
        }
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            abilityDisplay.Add(child.gameObject);
        }
    }

    public void Equip(string abilityName, int abilitySlot)
    {
        foreach (AbilityCooldown ability in abilityCooldowns)
        {
            ability.UpdateCooldown();
        }
        //// Create an instance of the type and put it on the character
        //Type abilityType = abilityName.GetType();
        ////player.curAbilities[abilitySlot] =(Ability) Activator.CreateInstance(abilityType, playerStats);

        //Sprite abilityIcon = Resources.Load<Sprite>("AbilityIcons/"+abilityName);

        //abilityDisplay[abilitySlot].GetComponent<Image>().overrideSprite = abilityIcon;
    }
}
