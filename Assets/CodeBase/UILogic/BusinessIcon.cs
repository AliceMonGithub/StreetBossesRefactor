using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessIcon : MonoBehaviour
{
    [SerializeField] private Image _businessImage;
    [SerializeField] private TMP_Text _earnText;

    

    public void Render(Business business)
    {
        _businessImage.sprite = business.Image;
        _earnText.text = business.Earning.ToString();
    }
}