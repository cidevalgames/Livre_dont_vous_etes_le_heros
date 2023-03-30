using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChapterStorage : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public int index = 0;
    public bool end = false;
    public string text;
    public Dictionary<string, int> choices = new Dictionary<string, int>();

    public void StoreChapter()
    {
        end = transform.childCount <= 2;

        text = textMesh.text;

        CreateStory createStory = FindObjectOfType<CreateStory>();

        for (int i = 0; i < transform.childCount - 2; i++)
        {
            string choiceText;

            choiceText = transform.GetChild(i + 2).GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text;

            int nextChapterIndex = 0;

            for (int j = 0; j < createStory.chapters.Count; j++)
            {
                if (createStory.chapters[j].GetChild(1).GetChild(0).GetComponent<Button>() == transform.GetChild(i + 2).GetChild(1).GetChild(1).GetComponent<LinkedButton>().linkedButton)
                {
                    nextChapterIndex = j;
                }
            }

            choices.Add(choiceText, nextChapterIndex);
        }
    }

    public void StoreFirstChapter()
    {
        end = false;

        text = textMesh.text;

        CreateStory createStory = FindObjectOfType<CreateStory>();

        for (int i = 0; i < transform.childCount - 3; i++)
        {
            string choiceText;

            choiceText = transform.GetChild(i + 3).GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text;

            int nextChapterIndex = 0;

            for (int j = 1; j < createStory.chapters.Count; j++)
            {
                if (createStory.chapters[j].GetChild(1).GetChild(0).GetComponent<Button>() == transform.GetChild(i + 3).GetChild(1).GetChild(1).GetComponent<LinkedButton>().linkedButton)
                {
                    nextChapterIndex = j;
                }
            }

            choices.Add(choiceText, nextChapterIndex);
        }
    }
}
