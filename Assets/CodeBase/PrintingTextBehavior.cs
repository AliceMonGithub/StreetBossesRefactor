using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.CodeBase
{
    public class PrintingTextBehavior : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [SerializeField] private AudioSource _printingSound;

        [SerializeField] private float _time;

        private void OnDisable()
        {
            _text.text = string.Empty;
        }

        public void PrintText(string text)
        {
            StartCoroutine(TextPrinting(text));
        }

        private IEnumerator TextPrinting(string text)
        {
            if(Time.deltaTime != 0)
            {
                yield return new WaitForSeconds(2f);

                _printingSound.enabled = true;
                _printingSound.Play();

                _text.text = string.Empty;

                foreach (var lastChar in text.ToCharArray())
                {
                    _text.text += lastChar;

                    yield return new WaitForSeconds(_time);
                }

                _printingSound.Stop();
                _printingSound.enabled = false;
            }
            else
            {
                _text.text = text;
            }
        }
    }
}