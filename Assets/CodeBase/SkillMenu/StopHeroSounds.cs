using HeroLogic;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.SkillMenu
{
    public class StopHeroSounds : MonoBehaviour
    {
        [SerializeField] private bool _onEnable = true;

        private void OnEnable()
        {
            if(_onEnable)
            {
                StopSound();
            }
        }

        public void StopSound()
        {
            var heroes = FindObjectsOfType<Hero>();

            for (int i = 0; i < heroes.Length; i++)
            {
                var audios = heroes[i].GetComponentsInChildren<AudioSource>();

                foreach (var audio in audios)
                {
                    audio.Stop();
                }
            }
        }
    }
}