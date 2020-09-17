using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BreathAbility")]
public class BreathAbility : Ability
{
    public GameObject particleSystemPrefab;
    public int damage;

    private BreathAttackTriggerable breathAtk;


    public override void Initialize(GameObject player)
    {
        // Initialize the ability with the stats in the scriptable object
        base.Initialize(player);

        breathAtk = player.AddComponent<BreathAttackTriggerable>();
        breathAtk.particleSystemPrefab = particleSystemPrefab;
        breathAtk.damage = damage;
       
    }


    public override void TriggerAbility()
    {
        breathAtk.BreathAttack();
    }

    public override void Unequip()
    {
        base.Unequip();

        breathAtk.Unequip();
        Destroy(breathAtk);
    }
}
