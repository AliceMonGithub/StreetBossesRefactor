using System;
using Assets.CodeBase;
using CodeBase.CharactersLogic;
using UnityEngine;
using UnityEngine.Rendering;

namespace CodeBase
{
    [RequireComponent(typeof(CharacterView))]
    public class Character : MonoBehaviour
    {
        public int MaxLevel => Upgrades.Length;

        public Sprite Image;

        [Space]

        public string Name;

        public int MaxHealth;

        public int Health;

        public int Damage;

        public int MoveSpeed;

        public int Level;

        public int AdditionalDamage;

        public int UpgradePercent;
        
        public float Distance = 1f;

        public int AutoCollectionTime;

        public bool Working;

        public UpgradeProperties[] Upgrades;

        [Space]

        [SerializeField] private CharacterFeature _characterFeature;

        [Space] 
        
        public CharacterWalk CharacterWalk;
        
        public CharacterView CharacterView;

        public PunchFX PunchFX;

        public delegate void HealthValueHandler();

        public event HealthValueHandler HealthValueChanged;

        private void OnValidate()
        {
            if (Damage < 0) Damage = 0;

            if (Health < 0) Health = 0;

            if (Level < 0) Level = 0;

            MaxHealth = Health;
        }

        public void TakeDamage(Character character)
        {
            Health -= character.Damage;
            
            HealthValueChanged?.Invoke();

            if (Health <= 0)
            {
                CharacterView.Die();

                character.CharacterView.Enemy = null;
            }
        }

        public Sprite GetCharacterFeatureSprite() => _characterFeature.FeatureSprite;
    }
}