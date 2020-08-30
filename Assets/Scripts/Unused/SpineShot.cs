//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpineShot : Ability
//{
//    GameObject spinePrefab;
//    public SpineShot(PlayerStats stats) : base(stats)
//    {
//        spinePrefab = Resources.Load("Prefabs/Abilities/Spine") as GameObject;
//        name = "SpineShot";
//    }

//    public override void Execute(GameObject player)
//    {
//        GameObject spine = Object.Instantiate(spinePrefab, player.transform.position, player.transform.rotation);

//        spine.GetComponent<Projectile>().SetSpeedMulti(stats.ProjSpeed);
//    }

//    public override void Unequip()
//    {

//    }
//}
