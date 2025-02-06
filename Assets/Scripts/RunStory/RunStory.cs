using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using StoryEditor;

public class RunStory : MonoBehaviour
{
    public string storyFolder;

    public List<ChapterSO> story = new List<ChapterSO>();

    [SerializeField] Transform scrollViewContent;
    [SerializeField] GameObject chapterPrefab;
    [SerializeField] GameObject lastChapterPrefab;
    [SerializeField] GameObject choiceButtonPrefab;

    string storyPath;

    private void Awake()
    {
        storyPath = "Assets/ScriptableObjects/" + storyFolder;

        story = AssetDatabase.FindAssets("t:Chapter", new string[] { storyPath })
            .Select(guid => AssetDatabase.LoadAssetAtPath<ChapterSO>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToList();
    }

    private void Start()
    {
        GameObject firstChapter = Instantiate(chapterPrefab, scrollViewContent);
        ChapterSO chapter = story[0];
        ChapterComponents chapterComponents = firstChapter.GetComponent<ChapterComponents>();

        TextMeshProUGUI chapterText = chapterComponents.chapterText;

        chapterComponents.chapterNumberText.text = "Chapitre 1 :";

        chapterText.text = chapter.text;

        List<Button> buttons = chapterComponents.choicesButtons;
        Transform buttonsParent = chapterComponents.buttonsParent;

        for (int i = 0; i < chapter.choices.Count; i++)
        {
            chapterText.text += "\n\n";
            chapterText.text += "Choix " + (i + 1) + " : " + chapter.choices[i].text;

            int index = AddListenerByChance(chapter.choices[i].probabilities);

            GameObject button = Instantiate(choiceButtonPrefab, buttonsParent);
            buttons.Add(button.GetComponent<Button>());

            buttons[i].onClick.AddListener(() => ClickedChoiceButton(index));

            button.GetComponentInChildren<TextMeshProUGUI>().text = "Choix " + (i + 1);
        }
    }

    public void ClickedChoiceButton(int index)
    {
        GameObject chapterInstance;
        ChapterSO chapter = story[index];

        if (chapter.end == false)
        {
            chapterInstance = Instantiate(chapterPrefab, scrollViewContent);

            ChapterComponents chapterComponents = chapterInstance.GetComponent<ChapterComponents>();
            Transform buttonsParent = chapterComponents.buttonsParent;
            TextMeshProUGUI chapterText = chapterComponents.chapterText;
            List<Button> buttons = chapterComponents.choicesButtons;

            chapterText.text = chapter.text;

            for (int i = 0; i < chapter.choices.Count; i++)
            {
                chapterText.text += "\n\n";
                chapterText.text += "Choix " + (i + 1) + " : " + chapter.choices[i].text;

                int choiceIndex = AddListenerByChance(chapter.choices[i].probabilities);

                GameObject choiceButton = Instantiate(choiceButtonPrefab, buttonsParent);
                buttons.Add(choiceButton.GetComponent<Button>());

                buttons[i].onClick.AddListener(() => ClickedChoiceButton(choiceIndex));
                choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = "Choix " + (i + 1);
            }

            chapterComponents.chapterNumberText.text = "Chapitre " + (index + 1) + " :";
        }
        else
        {
            chapterInstance = Instantiate(lastChapterPrefab, scrollViewContent);

            ChapterComponents chapterComponents = chapterInstance.GetComponent<ChapterComponents>();
            TextMeshProUGUI chapterText = chapterComponents.chapterText;

            chapterText.text = chapter.text;

            chapterComponents.reloadButton.onClick.AddListener(() => GameManager.Instance.ReloadGame());
            chapterComponents.quitButton.onClick.AddListener(() => GameManager.Instance.QuitGame());

            chapterComponents.chapterNumberText.text = "Chapitre " + (index + 1) + " :";
        }
    }

    public int AddListenerByChance(List<ChoiceProbability> choiceProbabilities)
    {
        int probabilitiesSum = 0;

        for (int i = 0; i < choiceProbabilities.Count; i++)
        {
            probabilitiesSum += choiceProbabilities[i].probability;
        }

        int randomInt = Random.Range(0, probabilitiesSum);

        foreach (ChoiceProbability choiceProbability in choiceProbabilities)
        {
            if (choiceProbability.probability >= randomInt)
            {
                return choiceProbability.index;
            }
        }

        return 0;
    }
}
