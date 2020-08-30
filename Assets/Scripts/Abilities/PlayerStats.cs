using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Multipliers for the abilities
    public float AOE;
    public float ProjSpeed;
    public float CooldownReduction;

    public PlayerStats()
    {
        AOE = 1;
        ProjSpeed = 1;
        CooldownReduction = 1;
    }
}
