using CodeBase;
using CodeBase.CameraLogic;
using Factories;
using SceneLogic;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;
using Zenject;

public class BusinessesPanel : MonoBehaviour
{
    [SerializeField] private UltEvent _onShow;
    [SerializeField] private UltEvent _onHide;

    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] private BusinessIcon _cell;

    [SerializeField] private Transform _grid;

    [SerializeField] private CameraMovement _cameraMovement;

    private readonly List<BusinessIcon> _icons = new List<BusinessIcon>();

    private BusinessIconFactory _factory = new BusinessIconFactory();

    private LoadCurtain _loadCurtain;

    [Inject]
    private void Construct(LoadCurtain loadCurtain)
    {
        _loadCurtain = loadCurtain;
    }

    public void Show()
    {
        _icons.ForEach(icon =>
        {
            icon.Stop();

            Destroy(icon.gameObject);

        });

        _icons.Clear();

        foreach (var business in _playerStats.Businesses.Value)
        {
            var icon = _factory.Create(_cell, _grid);

            icon.Play();
            icon.Render(business, this, _cameraMovement, _loadCurtain);

            _icons.Add(icon);
        }

        _onShow.Invoke();
    }

    public void Hide()
    {
        _onHide.Invoke();
    }
}
