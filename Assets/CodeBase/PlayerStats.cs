using System.Collections.Generic;
using UnityEngine;

namespace CodeBase
{
    [CreateAssetMenu]
    public class PlayerStats : ScriptableObject
    {
        public delegate void CoinsValueHandler(int value);
        public event CoinsValueHandler CoinsValueChanged;

        [SerializeField] private int _money;

        public List<Character> Characters;
        public List<Business> Businesses;

        [HideInInspector] public string CurrentSceneName;

        public int Money
        {
            get => _money;
            set
            {
                _money = value;

                CoinsValueChanged.Invoke(value);
            }
        }
    }
}