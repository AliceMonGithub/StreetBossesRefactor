using HeroLogic;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace CodeBase
{
    [CreateAssetMenu]
    public class PlayerStats : ScriptableObject
    {
        [SerializeField] private ReactiveProperty<int> _money;

        [SerializeField] List<Hero> _heroes;
        [SerializeField] List<Business> _businesses;

        public ReactiveProperty<int> Money => _money;

        public Business AttackingBusiness { get; set; }

        public List<Hero> Heroes => _heroes;
        public List<Business> Businesses => _businesses;

        public void AddMoney(int amount)
        {
            _money.Value += amount;
        }

        public bool TryFindBusiness(Business business) =>
            _businesses.Any(playerBusiness => playerBusiness == business);
    }
}