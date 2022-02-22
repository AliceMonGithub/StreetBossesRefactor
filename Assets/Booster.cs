using CodeBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu()]
    public class Booster : ScriptableObject
    {
        [SerializeField] private List<Character> _characters;
        [SerializeField] private int _cost;

        public List<Character> Characters => _characters;
        public int Cost => _cost;
    }
}