using CodeBase;
using Factories;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

public class BusinessesPanel : MonoBehaviour
{
    [SerializeField] private UltEvent _onShow;

    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] private BusinessIcon _cell;

    [SerializeField] private Transform _grid;

    private List<BusinessIcon> _icons = new List<BusinessIcon>();

    private BusinessIconFactory _factory = new BusinessIconFactory();

    public void Show()
    {
        _icons.ForEach(icon =>
        {
            icon.Stop();

            Destroy(icon.gameObject);

        });

        _icons.Clear();

        foreach (var business in _playerStats.Businesses)
        {
            var icon = _factory.Create(_cell, _grid);

            icon.Play();
            icon.Render(business);

            _icons.Add(icon);
        }

        _onShow.Invoke();
    }
}
