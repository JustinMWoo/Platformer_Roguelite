//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Dash : MovementAbility
//{
//    public Dash(PlayerStats stats) : base(stats)
//    {
//        // Increase player projectile speed 
//        stats.ProjSpeed +=1;

//        name = "Dash";

//    }

//    public override void Execute(GameObject player)
//    {
//        // Direction the player is facing
//        float facing = player.transform.right.x;

//        player.GetComponent<PlayerMovement>().SetVelocityXForTime(100*facing, 0.1f);
//    }

//    public override void Unequip()
//    {
//        stats.ProjSpeed -= 1;
//    }
//}
