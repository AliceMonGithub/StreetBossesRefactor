using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessIcon : MonoBehaviour
{
    [SerializeField] private Image _businessImage;
    [SerializeField] private TMP_Text _incomeText;

    

    public void Render(Business business)
    {
        _businessImage.sprite = business.Sprite;
        _incomeText.text = business.CollectAmount.ToString();
    }
}