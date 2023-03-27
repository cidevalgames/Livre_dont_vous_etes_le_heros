using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class RunStory : MonoBehaviour
{
    [SerializeField] string storyFolder;

    public List<Chapter> story = new List<Chapter>();

    [SerializeField] Transform scrollViewContent;
    [SerializeField] GameObject chapterPrefab;
    [SerializeField] GameObject lastChapterPrefab;
    [SerializeField] GameObject choiceButtonPrefab;

    string storyPath;

    private void Awake()
    {
        storyPath = "Assets/ScriptableObjects/" + storyFolder;

        story = AssetDatabase.FindAssets("t:Chapter", new string[] { storyPath })
            .Select(guid => AssetDatabase.LoadAssetAtPath<Chapter>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToList();
    }

    private void Start()
    {
        GameObject firstChapter = Instantiate(chapterPrefab, scrollViewContent);
        Chapter chapter = story[0];

        firstChapter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Chapitre 1 :";

        TextMeshProUGUI chapterText = firstChapter.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        chapterText.text = chapter.text;

        Transform buttonsParent = firstChapter.transform.GetChild(2);

        for (int i = 0; i < chapter.choices.Count; i++)
        {
            chapterText.text += "\n\n";
            chapterText.text += "Choix " + (i + 1) + " : " + chapter.choices[i].text;

            int index = chapter.choices[i].index;
            GameObject button = Instantiate(choiceButtonPrefab, buttonsParent);
            buttonsParent.GetChild(i).GetComponent<Button>().onClick.AddListener(() => ClickedChoiceButton(index));
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Choix " + (i + 1);
        }
    }

    public void ClickedChoiceButton(int index)
    {
        GameObject chapterInstance;
        Chapter chapter = story[index];

        if (chapter.end == false)
        {
            chapterInstance = Instantiate(chapterPrefab, scrollViewContent);

            Transform buttonsParent = chapterInstance.transform.GetChild(2);

            TextMeshProUGUI chapterText = chapterInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            chapterText.text = chapter.text;

            int i;

            for (i = 0; i < chapter.choices.Count; i++)
            {
                chapterText.text += "\n\n";
                chapterText.text += "Choix " + (i + 1) + " : " + chapter.choices[i].text;

                int choiceIndex = chapter.choices[i].index;
                GameObject choiceButton = Instantiate(choiceButtonPrefab, buttonsParent);
                buttonsParent.GetChild(i).GetComponent<Button>().onClick.AddListener(() => ClickedChoiceButton(choiceIndex));
                choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = "Choix " + (i + 1);
            }
        }
        else
        {
            chapterInstance = Instantiate(lastChapterPrefab, scrollViewContent);

            TextMeshProUGUI chapterText = chapterInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            chapterText.text = chapter.text;

            chapterInstance.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.ReloadGame());
            chapterInstance.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.QuitGame());
        }

        chapterInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Chapitre " + (index + 1) + " :";
    }
}
