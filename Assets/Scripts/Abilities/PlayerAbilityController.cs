using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    // 0 = movement ability, 1, 2, 3 = equipped
    public Ability[] curAbilities = new Ability[4];

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats stats = GetComponent<PlayerStats>();

        AbilityEquippingController.Current.Equip("Dash", 0);

        AbilityEquippingController.Current.Equip("SpineShot", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            curAbilities[0].Execute(gameObject);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            curAbilities[1].Execute(gameObject);
        }
    }
}
