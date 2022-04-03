using System.Collections;
using UnityEngine;

namespace Assets.CodeBase
{
    public class MaterialScroller : MonoBehaviour
    {
        [SerializeField] private float _scrollSpeed;

        [SerializeField] private Material _material;

        private float _offset;

        private void Update()
        {
            _offset += Time.deltaTime * _scrollSpeed;

            _material.mainTextureOffset = new Vector2(_offset, 0);     
        }
    }
}