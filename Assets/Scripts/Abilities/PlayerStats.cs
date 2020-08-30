using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Attributes")]
    public List<PlayerAttributes> attributes = new List<PlayerAttributes>();

    public PlayerAttributes Find(string name)
    {
        List<PlayerAttributes>.Enumerator playerAttr = attributes.GetEnumerator();

        while (playerAttr.MoveNext())
        {
            if(playerAttr.Current.attribute.name.ToString() == name)
            {
                return playerAttr.Current;
            }
        }

        return null;
    }
}
