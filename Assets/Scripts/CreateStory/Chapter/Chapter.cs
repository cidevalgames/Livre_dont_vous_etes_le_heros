using TMPro;
using UnityEngine;

namespace StoryEditor
{
    public class Chapter : MonoBehaviour
    {
        private ChapterSO _chapterSO;
        public ChapterSO ChapterSO { get { return _chapterSO; } set { _chapterSO = value; } }

        private ChapterUI _chapterUI;
        public ChapterUI ChapterUI { get { return _chapterUI; } }

        private void Awake()
        {
            _chapterUI = GetComponent<ChapterUI>();
        }
    }
}
