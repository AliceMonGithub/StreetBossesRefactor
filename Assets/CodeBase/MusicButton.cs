using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase
{
    public class MusicButton : MonoBehaviour
    {
        [SerializeField] private Image _image;

        [SerializeField] private Sprite _first;
        [SerializeField] private Sprite _second;

        private bool _pause;

        private void Awake()
        {
            bool pause = PlayerPrefs.GetInt("pause") == 1;

            if (pause)
            {
                _image.sprite = _second;
            }
            else
            {
                _image.sprite = _first;
            }

            AudioListener.volume = pause ? 0 : 1;

            _pause = pause;
        }

        public void Pressed()
        {
            _pause = !_pause;

            if (_pause)
            {
                _image.sprite = _second;
            }
            else
            {
                _image.sprite = _first;
            }

            AudioListener.volume = _pause ? 0 : 1;

            PlayerPrefs.SetInt("pause", _pause ? 1 : 0);
        }
    }
}