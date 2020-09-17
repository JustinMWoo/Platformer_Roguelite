using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAbilityTriggerable : MonoBehaviour
{
    PlayerStats stats;
    public float dashSpeed;
    public float dashDuration;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    public void ExecuteAbility()
    {
        GetComponent<PlayerMovement>().SetVelocityXForTime(dashSpeed, dashDuration);
    }
}
