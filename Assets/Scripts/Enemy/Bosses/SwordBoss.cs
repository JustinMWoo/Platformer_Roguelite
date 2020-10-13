using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBoss : Boss
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        fightStarted = false;
        bossAttacks = new List<BossAttack>
        {
            gameObject.AddComponent<DashBounceAttack>()
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (fightStarted)
            bossAttacks[0].Execute();
    }
}
