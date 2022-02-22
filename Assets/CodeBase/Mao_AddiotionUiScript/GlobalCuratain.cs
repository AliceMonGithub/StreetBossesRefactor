using System;
using UnityEngine;

namespace CodeBase.Mao_AddiotionUiScript
{
    public class GlobalCuratain : MonoBehaviour
    {
        public static GlobalCuratain Instance
        {
            get
            {
                if (_instance) return _instance;
                else
                {
                    _instance = new GameObject("Global Curatain").AddComponent<GlobalCuratain>();
                    return _instance;
                }
            }
        }
        private static GlobalCuratain _instance;
    }
}