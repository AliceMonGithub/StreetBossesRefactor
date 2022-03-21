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

        [SerializeField, HideInInspector] public Business AttackingBusiness;
        [SerializeField, HideInInspector] public string LastSceneName;

        public ReactiveProperty<int> Money => _money;

        public List<Hero> Heroes => _heroes;
        public List<Business> PlayerBusinesses => _businesses;

        public void AddMoney(int amount)
        {
            _money.Value += amount;
        }

        public void Add(Business Addbusiness)
        {
            if (PlayerBusinesses.Any(business => business == Addbusiness) == false)
            {
                PlayerBusinesses.Add(Addbusiness);
            }
        }

        public bool TryFindBusiness(Business business) =>
            _businesses.Any(playerBusiness => playerBusiness == business);
    }
}