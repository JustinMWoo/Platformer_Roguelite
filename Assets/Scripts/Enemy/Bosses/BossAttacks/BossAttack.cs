using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    private Boss boss;
    protected virtual void Start()
    {
        boss = GetComponent<Boss>();
    }

    public abstract void Execute();
    protected virtual void Done()
    {
        boss.NextAttack();
    }
}
