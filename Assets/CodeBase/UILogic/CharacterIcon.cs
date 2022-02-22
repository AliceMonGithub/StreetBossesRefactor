using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase
{
    public class CharacterIcon : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private Image _image;
        private Character _character;

        private CharacterPlacing _characterPlacing;
        private CharacterSpawnPoint _characterSpawnPoint;
        private bool _isSelect;

        public UltEvent Selected;
        public UltEvent Unselected;
        
        public void TrySelect()
        {
            if (_characterPlacing.CanAddCharacter())
                if (_isSelect == false)
                {
                    Select();

                    return;
                }

            if (_isSelect) Deselect();
        }

        private void Select()
        {
            _characterSpawnPoint = _characterPlacing.AddCharacter(_character);

            Selected.Invoke();
            
            _isSelect = true;
        }

        private void Deselect()
        {
            _characterPlacing.RemoveCharacter(_characterSpawnPoint.Character);

            _characterSpawnPoint = null;
            
            Unselected.Invoke();

            _isSelect = false;
        }

        public void Initialize(Character character, CharacterPlacing characterPlacing)
        {
            _name.text = character.Name;
            _levelText.text = character.Level.ToString();
            _healthText.text = character.Health.ToString();
            _damageText.text = character.Damage.ToString();
            _image.sprite = character.Image;
            _character = character;

            _characterPlacing = characterPlacing;
        }
    }
}