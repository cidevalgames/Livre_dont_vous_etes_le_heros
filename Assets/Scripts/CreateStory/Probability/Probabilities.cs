using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace StoryEditor
{
    public class Probabilities : MonoBehaviour
    {
        public List<ChoiceProbability> choiceProbabilities = new List<ChoiceProbability>();

        [SerializeField] GameObject probabilityPrefab;

        public void AddProbability(Transform probabilitiesParent)
        {
            GameObject probabilityGO = Instantiate(probabilityPrefab, probabilitiesParent);
            ChoiceProbability probability = probabilityGO.AddComponent<ChoiceProbability>();

            Button linkButton = probabilityGO.GetComponent<ProbabilityLinkButton>().linkButton;

            choiceProbabilities.Add(probability);
        }

        public List<ChoiceProbability> StoreProbabilities()
        {
            ChoiceProbability[] newProbabilities = new ChoiceProbability[choiceProbabilities.Count];

            for (int i = 0; i < choiceProbabilities.Count; i++)
            {
                ChoiceProbability probability = choiceProbabilities[i];

                ChoiceProbability newChoiceProbability = new ChoiceProbability();
                newChoiceProbability.probability = probability.ProbabilityUI.GetProbabilitySliderValue();

                foreach (Chapter chapter in FindObjectOfType<Chapters>().chapters)
                {
                    if (chapter.GetComponentInChildren<LinkedButton>().LinkButton == probability.GetComponent<ProbabilityLinkButton>().linkButton)
                    {
                        newChoiceProbability.index = chapter.ChapterSO.index;
                    }
                }

                newProbabilities[i] = newChoiceProbability;
            }

            return newProbabilities.ToList();
        }
    }
}
