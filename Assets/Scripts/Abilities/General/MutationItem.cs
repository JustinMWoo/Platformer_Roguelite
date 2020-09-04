using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MutationItem : MonoBehaviour
{
    public int numChoices =3;
    public int level = 0;

    List<Mutation> mutations;

    [SerializeField]
    GameObject mutationButtonPrefab;

    [SerializeField]
    GameObject availableMutationPanel;

    [SerializeField]
    GameObject mutationPanel;

    [SerializeField]
    TextMeshProUGUI prompt;

    // Start is called before the first frame update
    void Start()
    {
        prompt.gameObject.SetActive(false);

        mutationPanel.SetActive(false);

        mutations = new List<Mutation>();
        Mutation[] availableMutations = Resources.LoadAll<Mutation>("ScriptableObjects/GeneralMutations/Level" + level);
    
        for (int i = 0; i < numChoices; i++)
        {
            // Add random mutations for that level to mutation list
            mutations.Add(availableMutations[Random.Range(0, availableMutations.Length)]);
        }

        // Update mutation picking screen
        foreach (Mutation mutation in mutations)
        {
            // Change text and onclick event for each mutation button

            GameObject mutationButton = Instantiate(mutationButtonPrefab, availableMutationPanel.transform);

            mutationButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                foreach (StatMod mod in mutation.statMods)
                {
                    PlayerAttributes attr = AbilityEquippingController.Current.playerStats.Find(mod.affectedAttribute.name);

                    if (attr != null)
                    {
                        attr.AddMod(mod);
                    }
                }
                DeactivateMenu();
                Destroy(gameObject);
            });

            mutationButton.transform.Find("MutationSprite").GetComponent<Image>().overrideSprite = mutation.mutationSprite;
            mutationButton.transform.Find("MutationName").GetComponent<TextMeshProUGUI>().text = mutation.name;
            mutationButton.transform.Find("MutationDesc").GetComponent<TextMeshProUGUI>().text = mutation.mutationDescription;
            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Show prompt to activate pickup
            prompt.gameObject.SetActive(true);

            if (Input.GetButtonDown("MutationPickup")){
                // Show mutation picking screen
                mutationPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Hide prompt to activate pickup
            prompt.gameObject.SetActive(false);
        }
    }

    public void ActivateMenu()
    {
        mutationPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DeactivateMenu()
    {
        mutationPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
