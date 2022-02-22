using CodeBase;
using TMPro;
using UnityEngine;

public class CoinsRender : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _coinsTexts;
    [SerializeField] private PlayerStats _playerStats;

    private void Awake()
    {
        RenderCoins(_playerStats.Money);
    }

    private void OnEnable()
    {
        _playerStats.CoinsValueChanged += RenderCoins;
    }

    private void OnDisable()
    {
        _playerStats.CoinsValueChanged -= RenderCoins;
    }

    private void RenderCoins(int value)
    {
        foreach (var text in _coinsTexts) text.text = value.ToString();
    }
}