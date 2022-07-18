using TMPro;
using UnityEngine;

namespace CodeBase.UILogic
{
    public class TradeIcon : MonoBehaviour
    {
        [SerializeField] private TMP_Text _botNameText;
        [SerializeField] private TMP_Text _costText;

        [Space]

        [SerializeField] private PlayerStats _playerStats;

        private TradeRequest _request;
        
        public void Response()
        {
            _playerStats.Businesses.Value.Remove(_request.Business);

            _request.Bot.Businesses.Add(_request.Business);

            _request.Business.Security.Add(_request.Bot.StandartHero.HeroAttack);

            _request.Bot.Money -= _request.Cost;

            _playerStats.Money.Value += _request.Cost;

            _request.Business.UpgradeIcon.InitializeActionIcon();

            Destroy(gameObject);
        }

        public void Initialize(TradeRequest request)
        {
            _request = request;

            Render();
        }

        private void Render()
        {
            _botNameText.text = _request.Bot.Nickname;
            _costText.text = _request.Cost.ToString() + "$";
        }
    }
}