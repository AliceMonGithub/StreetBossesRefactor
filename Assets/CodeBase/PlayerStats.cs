using CodeBase.BotLogic;
using CodeBase.QuestLogic;
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

        [SerializeField] private ReactiveProperty<List<Hero>> _heroes;
        [SerializeField] private ReactiveProperty<List<Business>> _businesses;

        [Space]

        [SerializeField] private List<Business> _allBusinesses;
        [SerializeField] private List<Business> _neutralBusinesses;

        [Space]

        [SerializeField] private List<Quest> _quests;

        public List<TradeRequest> TradeRequests = new List<TradeRequest>();

        public Business AttackingBusiness;

        public string LastSceneName;

        public ReactiveProperty<int> Money => _money;

        public ReactiveProperty<List<Hero>> Heroes => _heroes;
        public ReactiveProperty<List<Business>> Businesses => _businesses;

        public List<Business> AllBusinesses => _allBusinesses;
        public List<Business> NeutralBusinesses => _neutralBusinesses;

        public List<Quest> Quests => _quests;

        public void AddQuest(Quest quest)
        {
            _quests.Add(quest);
        }

        public void SetNeutralBusinesses()
        {
            var businesses = new List<Business>();

            foreach (var business in _allBusinesses)
            {
                businesses.Add(business);
            }

            foreach (var business in _businesses.Value)
            {
                businesses.Remove(business);
            }

            businesses.RemoveAll(business => business.Bot != null);

            _neutralBusinesses = businesses;
        }

        public void Add(Business Addbusiness)
        {
            if (Businesses.Value.Any(business => business == Addbusiness) == false)
            {
                Businesses.Value.Add(Addbusiness);
            }
        }

        public bool TryFindBusiness(Business business) =>
            _businesses.Value.Any(playerBusiness => playerBusiness == business);

        public bool TryFindQuest(Quest quest) =>
            _quests.Any(playerQuest => playerQuest == quest);
    }
    

    public class TradeRequest
    {
        public Bot Bot { get; private set; }
        public Business Business { get; private set; }
        public int Cost { get; private set; }

        public TradeRequest(Bot bot, Business business, int cost)
        {
            Bot = bot;
            Business = business;
            Cost = cost;
        }
    }
}