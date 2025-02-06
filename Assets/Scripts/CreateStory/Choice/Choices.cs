using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StoryEditor
{
    public class Choices : MonoBehaviour
    {
        public List<Choice> choices = new List<Choice>();

        [SerializeField] GameObject choicePrefab;

        public void AddChoice()
        {
            GameObject choiceGO = Instantiate(choicePrefab, transform);
            Choice choice = choiceGO.AddComponent<Choice>();

            choices.Add(choice);
        }

        public List<Choice> StoreChoices()
        {
            Choice[] newChoices = new Choice[choices.Count];

            for (int i = 0; i < choices.Count; i++)
            {
                Choice currentChoice = choices[i];

                Choice newChoice = new Choice();

                newChoice.text = currentChoice.ChoiceUI.GetChoiceText();
                newChoice.probabilities = currentChoice.GetComponentInChildren<Probabilities>().StoreProbabilities();
                
                newChoices[i] = newChoice;
            }

            return newChoices.ToList();
        }
    }
}
