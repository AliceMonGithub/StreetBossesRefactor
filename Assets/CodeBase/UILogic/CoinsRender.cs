using CodeBase;
using TMPro;
using UnityEngine;
using UniRx;

public class CoinsRender : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _coinsTexts;
    [SerializeField] private PlayerStats _playerStats;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private int Money => _playerStats.Money.Value;

    private void OnEnable()
    {
        _playerStats.Money.Subscribe(action => RenderCoins()).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    private void RenderCoins()
    {
        foreach (var text in _coinsTexts) text.text = Money.ToString();
    }
}