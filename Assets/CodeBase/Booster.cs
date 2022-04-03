using CodeBase;
using HeroLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu()]
    public class Booster : ScriptableObject
    {
        [SerializeField] private List<Hero> _heroes;
        [SerializeField] private int _cost;

        public List<Hero> Heroes => _heroes;
        public int Cost => _cost;
    }
}