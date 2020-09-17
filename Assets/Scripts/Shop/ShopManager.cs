using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Selects and displays different major mutations from a pool
    // Use more scriptable objects for this and load from the resources folder


    public int numChoices = 4;
    [SerializeField]
    GameObject buttonPrefab;

    [SerializeField]
    GameObject availableMutationPanel;

    [SerializeField]
    TextMeshProUGUI currentExp;

    PlayerStats stats;
    MajorMutation[] availableMutations;
    List<MajorMutation> currentMutations;

 
    private void Start()
    {
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
        availableMutations = Resources.LoadAll<MajorMutation>("ScriptableObjects/MajorMutations");
        currentMutations = new List<MajorMutation>();
        UpdateCurrentExp();

        for (int i = 0; i < numChoices; i++)
        {
            // TODO: Make it so that same mutation doesnt appear twice
            currentMutations.Add(availableMutations[Random.Range(0, availableMutations.Length)]);
        }

        foreach (MajorMutation mutation in currentMutations)
        {
            // Change text and onclick event for each mutation button

            GameObject mutationButton = Instantiate(buttonPrefab, availableMutationPanel.transform);

            mutationButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                // If player has enough exp for the mutation
                if (mutation.cost <= stats.exp)
                {
                    stats.majorMutations[mutation.name] = true;
                    stats.exp -= mutation.cost;
                    // TODO: Create floating text that shows how much exp is lost
                    UpdateCurrentExp();
                    // TODO: Equip the visual item and make it so player cannot purchase item again
                }
                else
                {
                    // TODO: Not enough exp message
                }

            });

            mutationButton.transform.Find("MutationSprite").GetComponent<Image>().overrideSprite = mutation.mutationSprite;
            mutationButton.transform.Find("MutationName").GetComponent<TextMeshProUGUI>().text = mutation.name;
            mutationButton.transform.Find("MutationDesc").GetComponent<TextMeshProUGUI>().text = mutation.mutationDescription;
            mutationButton.transform.Find("MutationCost").GetComponent<TextMeshProUGUI>().text = "Cost: " + mutation.cost.ToString();
        }
    }
    private void UpdateCurrentExp()
    {
        currentExp.text = "Experience: " + stats.exp;
    }
}
