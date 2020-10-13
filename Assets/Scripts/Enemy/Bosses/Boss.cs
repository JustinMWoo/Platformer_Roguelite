using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for bosses, each boss is a subclass of this class
public class Boss : Enemy
{
    protected List<BossAttack> bossAttacks;

    protected Transform player;
    protected bool fightStarted;

    public Transform Player
    {
        get { return player; }
        set { player = value; }
    }

    public void StartFight()
    {
        fightStarted = true;
    }
}
