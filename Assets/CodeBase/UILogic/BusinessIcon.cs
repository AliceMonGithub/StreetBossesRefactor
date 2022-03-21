using CodeBase.CameraLogic;
using TMPro;
using UltEvents;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class BusinessIcon : MonoBehaviour
{
    [SerializeField] private UltEvent _hide;

    [SerializeField] private UltEvent _onPlay;
    [SerializeField] private UltEvent _onStop;

    [SerializeField] private Image _businessImage;
    [SerializeField] private TMP_Text _earnText;

    private BusinessesPanel _businessesPanel;

    private CameraMovement _camera;
    private Business _business;

    public void MoveToBusiness()
    {
        _business.BusinessImage.OnLight.Invoke();
        _camera.MoveToTarget(_business.BusinessImage.transform);

        _hide.Invoke();
    }

    public void Hide()
    {
        _hide.Invoke();
    }

    public void HideBusinessesPanel()
    {
        _businessesPanel.Hide();
    }

    public void Play()
    {
        _onPlay.Invoke();
    }

    public void Stop()
    {
        _onStop.Invoke();
    }

    public void Render(Business business, BusinessesPanel businessesPanel, CameraMovement camera)
    {
        _business = business;
        _businessesPanel = businessesPanel;
        _camera = camera;

        _businessImage.sprite = business.Image;
        _earnText.text = business.Earning.ToString();
    }
}