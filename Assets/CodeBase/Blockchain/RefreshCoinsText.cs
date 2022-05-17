using CodeBase;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.CodeBase.Blockchain
{
    public class RefreshCoinsText : MonoBehaviour
    {
        [SerializeField] private string _contract;

        [SerializeField] private PlayerStats _playerStats;

        [SerializeField] private ERC20BalanceOfExample _getBalance;

        [SerializeField] private float _refreshTime;

        private float _currentTime;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _refreshTime)
            {
                RefreshCoins();

                _currentTime = 0;
            }
        }

        private async void RefreshCoins()
        {
            var coins = await _getBalance.GetBalance(_contract);

            print("Coins amount = " + (int)coins);

            _playerStats.Money.Value = (int)coins;
        }
    }
}