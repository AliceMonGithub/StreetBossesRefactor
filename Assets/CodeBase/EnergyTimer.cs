using CodeBase;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase
{
    public class EnergyTimer : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            foreach(var hero in _playerStats.Heroes.Value)
            {
                if(hero.Energy != hero.MaxEnergy)
                {
                    hero.SpendCurrentEnergyTime(Time.deltaTime);
                }
            }
        }
    }
}