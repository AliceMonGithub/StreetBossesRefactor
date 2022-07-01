using CodeBase.NotificationsLogic;
using HeroLogic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.BotLogic
{
    public class Bot : MonoBehaviour
    {
        public event BusinessHandler OnLose;
        public delegate void BusinessHandler();

        [SerializeField] private List<Business> _businesses;
        [SerializeField] private List<Hero> _heroes;

        [SerializeField] private Hero[] _shopHeroes;

        [SerializeField] private NotificationPopup _popup;
        [SerializeField] private Hero _standartHero;

        [SerializeField] private PlayerStats _playerStats;

        [SerializeField] private string _nickname;

        [SerializeField] private string _streetName;
        [SerializeField] private float _stopAttackDirucation;

        public int Money;

        private bool _stopAttack;

        public List<Business> PlayerBusinesses => _playerStats.Businesses.Value.FindAll(business => business.StreetName == _streetName);

        public List<Business> Businesses => _businesses;
        public List<Hero> Heroes => _heroes;

        public string Nickname => _nickname;

        private void Awake()
        {
            OnLose += StopAttack;
        }

        private void Update()
        {
            if(Money >= 200)
            {
                _heroes.Add(_shopHeroes[Random.Range(0, _shopHeroes.Length)]);

                Money -= 200;
            }

            foreach(var business in _businesses)
            {
                business.Bot = this;
            }
        }

        private void OnDestroy()
        {
            OnLose -= StopAttack;
        }

        public void TryAttack()
        {
            if (_stopAttack) return;

            var businesses = PlayerBusinesses;

            if(businesses.Count > 0)
            {
                var business = businesses[Random.Range(0, businesses.Count)];

                if(business.Security.Count <= _heroes.Count)
                {
                    var attackHeroes = GetRandomHeroes();

                    var victory = business.SimulateAttack(attackHeroes);

                    if(victory)
                    {
                        print("Bot victory!");

                        foreach(var hero in business.Security)
                        {
                            _playerStats.Heroes.Value.Add(hero.Hero);

                            hero.Hero.SecurityBusiness = null;
                        }

                        _playerStats.Businesses.Value.Remove(business);

                        business.Security.Clear();

                        if(_heroes.Count >= 3)
                        {
                            var count = Mathf.Clamp(_heroes.Count - 2, 1, 3);
                            var heroes = _heroes.Take(count);

                            _heroes.RemoveAt(count);

                            foreach(var hero in heroes)
                            {
                                business.Security.Add(hero.HeroAttack);
                            }
                        }
                        else
                        {
                            business.Security.Add(_standartHero.HeroAttack);

                        }

                        _popup.ShowNotification("Your business has been taken over!", business);

                        _businesses.Add(business);

                        if(business.UpgradeIcon != null)
                        {
                            business.UpgradeIcon.InitializeActionIcon();
                        }
                        
                    }
                    else
                    {
                        print("Bot lose!");

                        _popup.ShowNotification("Security defeated the attack", business);
                    }
                }
            }
        }

        public List<Hero> GetRandomHeroes()
        {
            var heroes = new List<Hero>();

            for (int i = 0; i < 3; i++)
            {
                heroes.Add(_heroes[Random.Range(0, _heroes.Count)]);
            }

            return heroes;
        }

        public bool FindBusiness(Business business)
        {
            return _businesses.Any(botBusiness => botBusiness == business);
        }

        public void InvokeOnLose()
        {
            OnLose?.Invoke();
        }

        private void StopAttack()
        {
            print("On lose!"); 

            _stopAttack = true;

            Invoke(nameof(ResumeAttack), _stopAttackDirucation);
        }

        private void ResumeAttack()
        {
            print("Resume!");

            _stopAttack = false;
        }
    }
}