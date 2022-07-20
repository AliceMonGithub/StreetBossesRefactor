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

        [SerializeField] private int _attackChance;

        [SerializeField] private float _canAttackTime;
        [SerializeField] private bool _canAttack;

        public int Money;

        private bool _stopAttack;

        private Business _tradeBusiness;
        private int _cost;
        private bool _creatingTrade;

        public List<Business> PlayerBusinesses => _playerStats.Businesses.Value;
        public List<Business> NeutralBusinesses => _playerStats.NeutralBusinesses;

        public Hero StandartHero => _standartHero;

        public List<Business> Businesses => _businesses;
        public List<Hero> Heroes => _heroes;

        public string Nickname => _nickname;

        private void Awake()
        {
            Invoke(nameof(CanAttack), _canAttackTime);

            OnLose += StopAttack;
        }

        private void Update()
        {
            if(PlayerBusinesses.Count > 0)
            {
                var tradeBusiness = PlayerBusinesses[Random.Range(0, PlayerBusinesses.Count)];
                var cost = (int)((tradeBusiness.Cost + tradeBusiness.Earning * Random.Range(4, 7)) * (1 + 0.13f * tradeBusiness.Security.Count));

                if(_creatingTrade == false && _playerStats.TradeRequests.Any(trade => trade.Business == tradeBusiness) == false)
                {
                    if (cost <= Money)
                    {
                        _creatingTrade = true;

                        _cost = cost;
                        _tradeBusiness = tradeBusiness;

                        Invoke(nameof(CreateTrade), 25f);
                    }
                }
            }

            if (_heroes.Count < 10 && Money >= 200)
            {
                _heroes.Add(_shopHeroes[Random.Range(0, _shopHeroes.Length)]);

                Money -= 200;
            }

            foreach(var business in _businesses)
            {
                business.Bot = this;
            }
        }

        private void CreateTrade()
        {
            _playerStats.TradeRequests.Add(new TradeRequest(this, _tradeBusiness, _cost));

            _popup.ShowTrade(_cost);

            _creatingTrade = false;
        }

        private void OnDestroy()
        {
            OnLose -= StopAttack;
        }

        public void TryAttack()
        {
            if (_stopAttack) return;

            if (_canAttack == false) return;

            if (Random.Range(0, 101) > _attackChance) return;

            var businesses = new List<Business>();
            var isPlayerAttack = false;

            foreach (var business in _playerStats.AllBusinesses)
            {
                business.IsPlayerBusinessBot = false;
            }

            foreach(var business in NeutralBusinesses)
            {
                businesses.Add(business);
            }

            foreach (var business in PlayerBusinesses)
            {
                businesses.Add(business);

                business.IsPlayerBusinessBot = true;
            }

            if(businesses.Count > 0)
            {
                var business = businesses[Random.Range(0, businesses.Count)];

                if(business.Security.Count <= _heroes.Count)
                {
                    var attackHeroes = GetRandomHeroes();

                    if(business.IsPlayerBusinessBot)
                    {
                        isPlayerAttack = true;
                    }

                    var victory = business.SimulateAttack(attackHeroes);

                    if (victory)
                    {
                        print("Bot victory!");

                        foreach (var hero in business.Security)
                        {
                            hero.Hero.SecurityBusiness = null;
                        }

                        if (business.WorkingHero != null)
                        {
                            business.SetWorkingHero(null);
                        }

                        _playerStats.Businesses.Value.Remove(business);

                        business.Security.Clear();

                        if (_heroes.Count >= 3)
                        {
                            var count = Mathf.Clamp(_heroes.Count - 2, 1, 3);
                            var heroes = _heroes.Take(count);

                            _heroes.RemoveAt(count);

                            foreach (var hero in heroes)
                            {
                                business.Security.Add(hero.HeroAttack);
                            }
                        }
                        else
                        {
                            business.Security.Add(_standartHero.HeroAttack);

                        }

                        if (isPlayerAttack)
                        {
                            _popup.ShowNotification("Your business is under attack", "You lose", business);
                        }
                        else
                        {
                            print("Attack neutral win");
                        }

                        _businesses.Add(business);

                        if(business.UpgradeIcon != null)
                        {
                            business.UpgradeIcon.InitializeActionIcon();
                        }
                        
                    }
                    else
                    {
                        print("Bot lose!");

                        if (isPlayerAttack)
                        {
                            _popup.ShowNotification("Your business is under attack", "You win", business);
                        }
                        else
                        {
                            print("Attack neutral lose");
                        }
                    }
                }
            }
        }

        public bool BuyRequest(Business business, int cost)
        {
            var botCost = (int)((business.Cost + business.Earning * Random.Range(4, 7)) * (1 + 0.13f * business.Security.Count));

            if (cost >= botCost)
            {
                foreach (var hero in business.Security)
                {
                    _heroes.Add(hero.Hero);
                }

                business.Security.Clear();

                _playerStats.Money.Value -= botCost;
                _playerStats.Businesses.Value.Add(business);

                _businesses.Remove(business);

                return true;
            }

            return false;
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

        private void CanAttack()
        {
            print("Now can attack");

            _canAttack = true;
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