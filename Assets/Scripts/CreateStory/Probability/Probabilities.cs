using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Probabilities : MonoBehaviour
{
    public Dictionary<GameObject, ChoiceProbability> choiceProbabilities = new Dictionary<GameObject, ChoiceProbability>();

    [SerializeField] GameObject probabilityPrefab;

    public void AddProbability(Transform probabilitiesParent)
    {
        GameObject probability = Instantiate(probabilityPrefab, probabilitiesParent);

        Button linkButton = probability.GetComponent<ProbabilityLinkButton>().linkButton;
        linkButton.onClick.AddListener(() => FindObjectOfType<LinksManager>().LinkButton(linkButton));

        choiceProbabilities.Add(probability, new ChoiceProbability());
    }

    public List<ChoiceProbability> StoreProbabilities()
    {
        foreach (KeyValuePair<GameObject, ChoiceProbability> probability in choiceProbabilities.ToList())
        {
            ChoiceProbability newChoiceProbability = new ChoiceProbability();
            newChoiceProbability.probability = (int) probability.Key.GetComponentInChildren<ProbabilitySlider>().slider.value;

            foreach (KeyValuePair<GameObject, Chapter> chapter in FindObjectOfType<Chapters>().chapters)
            {
                if (chapter.Key.GetComponentInChildren<LinkedButton>().linkedButton == probability.Key.GetComponent<ProbabilityLinkButton>().linkButton)
                {
                    newChoiceProbability.index = chapter.Value.index;
                }
            }

            choiceProbabilities[probability.Key] = newChoiceProbability;
        }

        return choiceProbabilities.Values.ToList<ChoiceProbability>();
    }
}
