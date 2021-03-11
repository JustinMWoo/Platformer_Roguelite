using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBoss : Boss
{
    private void Awake()
    {
        bossAttacks = new List<BossAttack>
        {
            gameObject.AddComponent<DashBounceAttack>()
        };
    } 
}
