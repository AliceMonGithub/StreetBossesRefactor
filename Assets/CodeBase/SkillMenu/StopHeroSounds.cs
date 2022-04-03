using HeroLogic;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.SkillMenu
{
    public class StopHeroSounds : MonoBehaviour
    {
        private void OnEnable()
        {
            var heroes = FindObjectsOfType<Hero>();

            for (int i = 0; i < heroes.Length; i++)
            {
                var audios = heroes[i].GetComponentsInChildren<AudioSource>();

                foreach(var audio in audios)
                {
                    audio.Stop();
                }
            }
        }
    }
}