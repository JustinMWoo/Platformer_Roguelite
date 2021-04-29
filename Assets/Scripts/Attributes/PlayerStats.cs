using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // TODO: Maybe get all these from a folder like how the major mutations are loaded and have a default value for the attributes when creating the attribute
    [Header("Player Attributes")]
    public List<PlayerAttributes> attributes = new List<PlayerAttributes>();

    [HideInInspector]
    public Dictionary<string, bool> majorMutations;

    //[HideInInspector]
    public int exp;

    private static PlayerStats _current;
    public static PlayerStats Current { get { return _current; } }

    // TODO: Weird place to put this, probably make a player manager class that controls the loading of the player between scenes
    private void Awake()
    {
        if (_current != null && _current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _current = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Initialize each potential major mutation
        majorMutations = new Dictionary<string, bool>();
        MajorMutation[] tempMutations =  Resources.LoadAll<MajorMutation>("ScriptableObjects/MajorMutations");

        foreach (MajorMutation mut in tempMutations)
        {
            majorMutations.Add(mut.name, false);
        }
    }

    public PlayerAttributes Find(string name)
    {
        List<PlayerAttributes>.Enumerator playerAttr = attributes.GetEnumerator();

        while (playerAttr.MoveNext())
        {
            if (playerAttr.Current.attribute.name.ToString() == name)
            {
                return playerAttr.Current;
            }
        }

        return null;
    }
}
