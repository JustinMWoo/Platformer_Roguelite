using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for bosses, each boss is a subclass of this class
public class Boss : Enemy
{
    protected List<BossAttack> bossAttacks;

    protected Transform player;
    protected bool fightStarted;
    protected int currentMoveIndex;

    [SerializeField]
    protected BossRoom room;

    public Transform Player
    {
        get { return player; }
        set { player = value; }
    }

    public void StartFight()
    {
        fightStarted = true;
    }

    protected override void Start()
    {
        base.Start();
        NextAttack();
        fightStarted = false;
    }
    void Update()
    {
        if (fightStarted)
        {
            bossAttacks[currentMoveIndex].Execute();
        }
    }
    public void NextAttack()
    {
        currentMoveIndex = Random.Range(0, bossAttacks.Count);
    }
    protected override void Die()
    {
        base.Die();
        GameManager.Current.bossAlive = false;
        room.EndBossFight();
    }
}
