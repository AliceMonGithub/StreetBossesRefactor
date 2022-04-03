using CodeBase.QuestLogic;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.CodeBase
{
    public class QuestIcon : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        [SerializeField] private TMP_Text _progress;

        private Quest _quest;

        private void Render()
        {
            _name.text = _quest.Name;
            _description.text = _quest.Description;
            
            _progress.text = (_quest.Complete ? _quest.MaxProgress : _quest.Progress) + "/" + _quest.MaxProgress;
        }

        public void Initialize(Quest quest)
        {
            _quest = quest;

            Render();
        }
    }
}