using CodeBase;
using UltEvents;
using UnityEngine;

public class BusinessesPanel : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] private BusinessIcon _cell;

    [SerializeField] private Transform _grid;

    [SerializeField] private UltEvent _onShow;
    [SerializeField] private UltEvent _onHide;
    
    public void Show()
    {
        foreach (Transform icon in _grid)
        {
            Destroy(icon.gameObject);
        }
        _playerStats.Businesses.ForEach(business => Instantiate(_cell, _grid).Render(business));
        _onShow.Invoke();
    }

    public void Hide() => _onHide.Invoke();
}
