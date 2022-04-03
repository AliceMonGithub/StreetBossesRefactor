using Assets.CodeBase;
using System.Collections;
using UltEvents;
using UniRx;
using UnityEngine;

namespace CodeBase.QuestLogic
{
    public abstract class Quest : ScriptableObject
    {
        [Header("Quest properties")]
        [SerializeField] private UltEvent _onComplete;

        [Space]

        [SerializeField] private string _name;
        [TextArea, SerializeField] private string _description;

        [SerializeField] private int _progress;
        [SerializeField] private int _maxProgress;

        [Space]

        public PlayerStats PlayerStats;

        public bool Complete;

        public UltEvent OnComplete => _onComplete;

        public string Name => _name;
        public string Description => _description;

        public int Progress { get => _progress; set => _progress = value; }
        public int MaxProgress { get => _maxProgress; set => _maxProgress = value; }

        public abstract void Active();
        public abstract void CheckComplete();
    }
}