using CodeBase;
using System;
using UltEvents;
using UnityEngine;

public class CharacterSelectMenu : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private UpgradeMenu _updateMenu;

    [Space]

    [SerializeField] private CharacterSelectIcon _unselectCharacterIcon;
    [SerializeField] private CharacterSelectIcon _characterSelectIcon;
    [SerializeField] private Transform _grid;

    public UltEvent OnShow;
    public UltEvent OnHide;

    public void RenderCharacters()
    {
        gameObject.SetActive(true);

        ClearIcons();

        var unselectIcon = Instantiate(_unselectCharacterIcon, _grid);

        unselectIcon.Initialize(_updateMenu.CurrentBusiness.WorkingCharacter, _updateMenu);

        foreach (Character character in _playerStats.Characters)
        {
            if (character.Working) continue;

            var icon = Instantiate(_characterSelectIcon, _grid);

            icon.Initialize(character, _updateMenu);
        }
        
        OnShow.Invoke();
    }

    public void Hide() => OnHide.Invoke();

    private void ClearIcons()
    {
        foreach(Transform child in _grid)
        {
            Destroy(child.gameObject);
        }
    }
}
