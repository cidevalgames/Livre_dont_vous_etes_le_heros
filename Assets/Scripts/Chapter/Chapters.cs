using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Chapters : MonoBehaviour
{
    public Dictionary<GameObject, Chapter> chapters = new Dictionary<GameObject, Chapter>();

    [SerializeField] GameObject chapterPanelPrefab;

    private void Awake()
    {
        GameObject[] setupChapters = GameObject.FindGameObjectsWithTag("Chapter");

        foreach (GameObject chapter in setupChapters)
        {
            chapters.Add(chapter, new Chapter());
        }
    }

    public void AddChapter()
    {
        GameObject chapter = Instantiate(chapterPanelPrefab, transform);

        ChapterButtons buttons = chapter.GetComponent<ChapterButtons>();

        Button linkButton = buttons.linkButton;
        linkButton.onClick.AddListener(() => LinksManager.Instance.LinkButton(linkButton));

        Button addChoiceButton = buttons.addChoiceButton;
        addChoiceButton.onClick.AddListener(() => chapter.GetComponentInChildren<Choices>().AddChoice());

        chapters.Add(chapter, new Chapter());
    }

    public List<Chapter> StoreChapters()
    {
        int i = 0;

        foreach (KeyValuePair<GameObject, Chapter> chapter in chapters.ToList())
        {
            Chapter newChapter = new Chapter();
            newChapter.index = i;
            newChapter.end = false;
            newChapter.text = chapter.Key.GetComponent<ChapterText>().text.text;

            chapters[chapter.Key] = newChapter;

            i++;
        }

        foreach (KeyValuePair<GameObject, Chapter> chapter in chapters)
        {
            chapter.Value.choices = chapter.Key.GetComponentInChildren<Choices>().StoreChoices();
        }

        return chapters.Values.ToList<Chapter>();
    }
}
