using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UltEvents;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeanPointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private LeanManualAnimation OnEnterTransition;
    [SerializeField] private UltEvent OnEnter;
    [SerializeField] private LeanManualAnimation OnExitTransition;
    [SerializeField] private UltEvent OnExit;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter.Invoke();
        OnEnterTransition?.BeginTransitions();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExit.Invoke();
        OnExitTransition?.BeginTransitions();
    }
}
