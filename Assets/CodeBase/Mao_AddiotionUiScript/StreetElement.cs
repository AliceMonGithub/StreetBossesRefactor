using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Mao_AddiotionUiScript
{
    public class StreetElement : MonoBehaviour
    {
        [SerializeField] private SelectingStreetMenu _selectingStreetMenu;
        public string NameToView;
        public Sprite Photo;
        [SerializeField] private string _sceneName;
        [SerializeField] private TextMeshProUGUI _labelNameStreet;
        [SerializeField] private Image _photoStreet;

        private void Awake()
        {
            _photoStreet.sprite = Photo;
            _labelNameStreet.text = NameToView;
        }

        public void Load() => _selectingStreetMenu.SelectStreet(_sceneName);
    }
}