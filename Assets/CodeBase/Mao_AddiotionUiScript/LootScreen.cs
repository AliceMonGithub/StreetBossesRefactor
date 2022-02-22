using System.Linq;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Mao_AddiotionUiScript
{
    public class LootScreen : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Transform _pointForLoot;
        public UltEvent OnShow;
        public UltEvent OnHide;

        public void SetColorBackground(Color color)
        {
            _image.color = color;
        }

        public void Show() => OnShow.Invoke();

        public void Hide()
        {
            foreach (var transform1 in _pointForLoot.GetComponentsInChildren<Transform>().Except(new []{_pointForLoot})) 
                Destroy(transform1.gameObject);
            if(gameObject.activeSelf)
                OnHide.Invoke();
        }

        public void SetNewElement(Character character, LootOfChest lootTemplate)
        {
            foreach (var transform1 in _pointForLoot.GetComponentsInChildren<Transform>().Except(new []{_pointForLoot})) 
                Destroy(transform1.gameObject);
            
            var instance = Instantiate(lootTemplate, _pointForLoot);

            instance.CharacterImage.sprite = character.Image;
            instance.CharacterName.text = character.Name;

            instance.InitPoint(_pointForLoot);
            instance.Show(this);
        }
    }
}