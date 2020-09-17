using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityItem : MonoBehaviour
{
    public Ability ability;
    public GameObject abilityCanvas;
    public TextMeshProUGUI abilityNameUI;
    public TextMeshProUGUI abilityDescUI;
    public Image abilitySpriteUI;
    public Rigidbody2D rb;


    [SerializeField] private LayerMask whatIsGround;
    bool grounded;

    private void Start()
    {
        Vector2 force = new Vector2(Random.Range(-200, 200), 200);
        rb.AddForce(force);

        // Call this from unit dropping and remove start
        Initialize();
    }

    public void Initialize()
    {
        abilityCanvas.SetActive(false);
        abilityNameUI.text = ability.aName;
        abilityDescUI.text = ability.aDescription;

        abilitySpriteUI.overrideSprite = ability.aSprite;
    }



    private void FixedUpdate()
    {
        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (grounded && collision.gameObject.CompareTag("Player"))
        {
            AbilityController.Current.AddEquippableAbility(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AbilityController.Current.RemoveEquippableAbility(gameObject);
        }
    }

    public void DisplayAbility()
    {
        abilityCanvas.SetActive(true);
    }

    public void HideAbility()
    {
        abilityCanvas.SetActive(false);
    }
}
