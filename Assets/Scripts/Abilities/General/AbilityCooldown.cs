using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    public string abilityButtonAxisName = "Fire1";
    public Image darkMask;
    public TextMeshProUGUI cooldownTextDisplay;

    public bool movementAbility = false;

    [SerializeField] private Ability ability;
    [SerializeField] private GameObject player;
    private Image buttonImage;
    private float cooldownDuration;
    private float nextReadyTime;
    private float cooldownTimeLeft;

    public bool initialize;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerStats.Current.gameObject;
        if (initialize)
        {
            Initialize(Instantiate(ability), player);
        }
    }

    public void Initialize(Ability selectedAbility, GameObject player)
    {
        ability = selectedAbility;
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = ability.aSprite;
        darkMask.sprite = ability.aSprite;
        UpdateCooldown();
        ability.Initialize(player);
        AbilityReady();
    }

    // Update is called once per frame
    void Update()
    {

        bool cooldownComplete = (Time.time > nextReadyTime);
        if (cooldownComplete)
        {
            AbilityReady();
            if (Input.GetButtonDown(abilityButtonAxisName))
            {
                // If the player is touching an ability and presses the button
                if (AbilityController.Current.IsTouchingAbility())
                {
                    // equip item
                    AbilityController.Current.Equip(this);
                }
                else if (ability)
                {
                    ButtonTriggered();
                }
            }
        }
        else
        {
            Cooldown();
        }

    }

    private void AbilityReady()
    {
        cooldownTextDisplay.enabled = false;
        darkMask.enabled = false;
        //TODO: This is inefficient, only call when potentially changing cooldown value (changing equipment)
        UpdateCooldown();
    }

    private void Cooldown()
    {
        cooldownTimeLeft -= Time.deltaTime;
        float roundedCD = Mathf.Round(cooldownTimeLeft);
        cooldownTextDisplay.text = roundedCD.ToString();
        darkMask.fillAmount = (cooldownTimeLeft / cooldownDuration);
    }

    private void ButtonTriggered()
    {
        nextReadyTime = cooldownDuration + Time.time;
        cooldownTimeLeft = cooldownDuration;
        darkMask.enabled = true;
        cooldownTextDisplay.enabled = true;

        ability.TriggerAbility();
    }

    public void UpdateCooldown()
    {
        // Only run if there is an ability assigned
        if (ability)
            cooldownDuration = ability.aBaseCooldown * player.GetComponent<PlayerStats>().Find("Cooldown").Value;
    }

    public Ability GetAbility()
    {
        return ability;
    }
}
