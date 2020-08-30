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

    [SerializeField] private Ability ability;
    [SerializeField] private GameObject player;
    private Image buttonImage;
    private float cooldownDuration;
    private float nextReadyTime;
    private float cooldownTimeLeft;


    // Start is called before the first frame update
    void Start()
    {
        Initialize(ability, player);
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
                ButtonTriggered();
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
        cooldownDuration = ability.aBaseCooldown * player.GetComponent<PlayerStats>().Find("Cooldown").amount;
    }
}
