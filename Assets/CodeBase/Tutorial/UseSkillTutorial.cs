using Assets.CodeBase.SkillMenu;
using Assets.CodeBase.TutorialLogic;
using System.Collections;
using UltEvents;
using UnityEngine;

namespace Assets.CodeBase.Tutorial
{
    public class UseSkillTutorial : MonoBehaviour
    {
        [SerializeField] private UltEvent _onCloseDialog;
        [SerializeField] private UltEvent _onShow;

        [Space]

        [TextArea, SerializeField] private string _startupText;

        [SerializeField] private float _showTime;

        [Space]

        [SerializeField] private TutorialInfo _tutorialInfo;

        [SerializeField] private Dialog _dialog;

        public void Active()
        {
            Invoke(nameof(ShowTutorial), _showTime);
        }

        public void RenderTringle()
        {
            FindObjectOfType<SkillIcon>().RenderTringle();
        }

        private void ShowTutorial()
        {
            if (_tutorialInfo.UseSkillHelp)
            {
                _dialog.OnCloseDialog = _onCloseDialog;

                _dialog.Show(_startupText);

                _onShow.Invoke();

                _tutorialInfo.UseSkillHelp = false;
            }
        }
    }
}