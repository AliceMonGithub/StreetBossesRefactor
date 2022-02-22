using CodeBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectIcon : MonoBehaviour
{
    [SerializeField] private TMP_Text _characterNameText;
    [SerializeField] private Image _characterImage;
    [SerializeField] private Image _characterFeatureImage;

    private PlayerStats _playerStats;
    private UpgradeMenu _upgradeMenu;
    private Character _character;

    public void Initialize(Character character, UpgradeMenu upgradeMenu)
    {
        _upgradeMenu = upgradeMenu;
        _character = character;

        _playerStats = _upgradeMenu.PlayerStats;

        if (_characterNameText != null)
        {
            _characterNameText.text = character.Name;

            _characterImage.sprite = character.Image;

            var sprite = character.GetCharacterFeatureSprite();

            if (sprite != null)
            {
                _characterFeatureImage.sprite = sprite;

                _characterFeatureImage.enabled = true;

                return;
            }

            _characterFeatureImage.enabled = false;
        }
    }

    public void Select()
    {
        if(_upgradeMenu.CurrentBusiness.WorkingCharacter != null)
        {
            _upgradeMenu.CurrentBusiness.WorkingCharacter.Working = false;
        }

        _upgradeMenu.CurrentBusiness.WorkingCharacter = _character;

        _upgradeMenu.CurrentBusiness.WorkingCharacter.Working = true;

        _upgradeMenu.Show(_upgradeMenu.CurrentBusiness, _playerStats, false);

        _upgradeMenu.CharacterSelectMenu.Hide();
    }

    public void UnSelect()
    {
        if (_upgradeMenu.CurrentBusiness.WorkingCharacter != null)
        {
            _upgradeMenu.CurrentBusiness.WorkingCharacter.Working = false;
        }

        _upgradeMenu.CurrentBusiness.WorkingCharacter = null;

        _upgradeMenu.Show(_upgradeMenu.CurrentBusiness, _playerStats, false);

        _upgradeMenu.CharacterSelectMenu.Hide();
    }
}