using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

public class BusinessIcon : MonoBehaviour
{
    [SerializeField] private UltEvent _onPlay;
    [SerializeField] private UltEvent _onStop;

    [SerializeField] private Image _businessImage;
    [SerializeField] private TMP_Text _earnText;

    public void Play()
    {
        _onPlay.Invoke();
    }

    public void Stop()
    {
        _onStop.Invoke();
    }

    public void Render(Business business)
    {
        _businessImage.sprite = business.Image;
        _earnText.text = business.Earning.ToString();
    }
}