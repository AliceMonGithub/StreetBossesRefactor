using CodeBase;
using System.Collections;
using UnityEngine;

namespace Zenject
{
    public class BackgroundInstaller : MonoInstaller
    {
        [SerializeField] private PlayerStats _stats;

        [Space]

        [SerializeField] private SpriteRenderer _background;

        public override void InstallBindings()
        {
            _background.sprite = _stats.AttackingBusiness.Background;
        }
    }
}