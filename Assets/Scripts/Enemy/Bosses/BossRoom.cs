using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField]
    private Boss boss;
    [SerializeField]
    private GameObject roomDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.Player = collision.transform;
            StartBossFight();
        }
    }

    private void StartBossFight()
    {
        boss.StartFight();
        roomDoor.SetActive(true); // TODO: change to moveing door? Or add an animation to the door that closes automatically
    }

    public void EndBossFight()
    {
        roomDoor.SetActive(false);
    }
}
