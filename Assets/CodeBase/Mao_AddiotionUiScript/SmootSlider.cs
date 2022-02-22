using DG.Tweening;
using Lean.Transition.Method;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Mao_AddiotionUiScript
{
    public class SmootSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;
        public UltEvent OnStart;
        public UltEvent OnFinish;

        public void Smooth(float moveTo)
        {
            OnStart.Invoke();
            _slider.DOValue(moveTo, _duration).SetEase(_ease).OnComplete(() => OnFinish?.Invoke());
        }
        
        public void Smooth(int moveTo)
        {
            OnStart.Invoke();
            _slider.DOValue(moveTo, _duration).SetEase(_ease).OnComplete(() => OnFinish?.Invoke());
        }
    }
}