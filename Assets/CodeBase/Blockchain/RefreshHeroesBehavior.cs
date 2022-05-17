using CodeBase;
using HeroLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Blockchain
{
    public class RefreshHeroesBehavior : MonoBehaviour
    {
        [SerializeField] private string[] _tokensID;

        [SerializeField] private Hero[] _heroesPrefab;

        [SerializeField] private float _refreshTime;

        [SerializeField] private PlayerStats _playerStats;

        [SerializeField] private ERC1155BalanceOfExample _getBalance;

        private float _currentTime;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if(_currentTime >= _refreshTime)
            {
                RefreshHeroes();

                _currentTime = 0;
            }
        }

        private async void RefreshHeroes()
        {
            var heroes = new List<Hero>();

            for (int i = 0; i < _tokensID.Length; i++)
            {
                var result = await _getBalance.CheckBalance(_tokensID[i]);

                if(result == true)
                {
                    heroes.Add(_heroesPrefab[i]);
                }
            }

            _playerStats.Heroes.Value = heroes;
        }
    }
}