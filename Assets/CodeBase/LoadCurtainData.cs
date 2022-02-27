using System.Collections;
using UnityEngine;

namespace SceneLogic
{
    [CreateAssetMenu]
    public class LoadCurtainData : ScriptableObject
    {
        [SerializeField] private LoadCurtain _loadCurtain;

        public LoadCurtain Curtain => _loadCurtain;
    }
}