using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Choices : MonoBehaviour
{
    public Dictionary<GameObject, Choice> choices = new Dictionary<GameObject, Choice>();

    [SerializeField] GameObject choicePrefab;

    public void AddChoice()
    {
        GameObject choice = Instantiate(choicePrefab, transform);

        choices.Add(choice, new Choice());
    }

    public List<Choice> StoreChoices()
    {
        foreach (KeyValuePair<GameObject, Choice> choice in choices.ToList())
        {
            Choice newChoice = new Choice();
            newChoice.text = choice.Key.GetComponent<ChoiceText>().text.text;

            newChoice.probabilities = choice.Key.GetComponentInChildren<Probabilities>().StoreProbabilities();

            choices[choice.Key] = newChoice;
        }

        return choices.Values.ToList<Choice>();
    }
}
