using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using StoryEditor;
using static UnityEngine.ScriptableObject;

public class Chapters : MonoBehaviour
{
    public List<Chapter> chapters = new();

    [SerializeField] GameObject chapterPanelPrefab;

    private void Awake()
    {
        Chapter[] setupChapters = FindObjectsOfType<Chapter>();

        foreach (Chapter chapter in setupChapters)
        {
            chapter.ChapterSO = CreateInstance(typeof(ChapterSO)) as ChapterSO;
        }
    }

    public void AddChapter()
    {
        GameObject chapter = Instantiate(chapterPanelPrefab, transform);
        Chapter newChapter = chapter.GetComponent<Chapter>();

        newChapter.ChapterSO = CreateInstance(typeof(ChapterSO)) as ChapterSO;
        newChapter.ChapterUI.Initialize();
    }

    public List<ChapterSO> StoreChapters()
    {
        List<ChapterSO> chaptersToStore = new();

        for (int i = 0; i < chapters.Count; i++)
        {
            ChapterSO newChapter = CreateInstance(typeof(ChapterSO)) as ChapterSO;
            Chapter chapter = chapters[i];

            newChapter.index = i;
            newChapter.end = false;
            newChapter.text = chapter.ChapterUI.GetChapterText();

            chapter.ChapterSO = newChapter;
        }

        foreach (Chapter chapter in chapters)
        {
            ChapterSO currentChapterSO = chapter.ChapterSO;

            currentChapterSO.choices = chapter.GetComponentInChildren<Choices>().StoreChoices();

            chaptersToStore.Add(currentChapterSO);
        }

        return chaptersToStore;
    }
}
