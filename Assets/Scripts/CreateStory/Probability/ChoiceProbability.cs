using System;
using UnityEngine;

namespace StoryEditor
{
    public class ChoiceProbability : MonoBehaviour
    {
        [Range(0, 100)]
        public int probability = 100;
        [Min(0)]
        public int index;

        private ProbabilityUI _probabilityUI;
        public ProbabilityUI ProbabilityUI { get { return _probabilityUI; } }

        private void Awake()
        {
            _probabilityUI = GetComponent<ProbabilityUI>();
        }
    }
}