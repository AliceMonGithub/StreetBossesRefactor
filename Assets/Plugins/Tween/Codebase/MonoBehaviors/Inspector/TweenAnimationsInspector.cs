using UnityEditor;
using UnityEngine;

namespace Tween.Inspector
{
    [CustomEditor(typeof(TweenAnimations))]
    public class TweenAnimationsInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var animations = target as TweenAnimations;

            GUILayout.BeginHorizontal();

            if(GUILayout.Button("Play"))
            {
                animations.Play();
            }

            if (GUILayout.Button("Stop"))
            {
                animations.Stop();
            }

            GUILayout.EndHorizontal();
        }
    }
}