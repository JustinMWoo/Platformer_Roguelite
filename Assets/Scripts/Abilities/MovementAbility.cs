using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MovementAbility")]
public class MovementAbility : Ability
{
    public float dashSpeed = 100f;
    public float dashDuration = 0.1f;

    private MovementAbilityTriggerable moveTrigger;

    public override void Initialize(GameObject player)
    {
        base.Initialize(player);
        moveTrigger = player.GetComponent<MovementAbilityTriggerable>();
        moveTrigger.dashSpeed = dashSpeed;
        moveTrigger.dashDuration = dashDuration;
    }

    public override void TriggerAbility()
    {
        moveTrigger.ExecuteAbility();
    }

    public override void Unequip()
    {
        base.Unequip();
    }
}
