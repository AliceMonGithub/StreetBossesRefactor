using CodeBase;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

internal class FinishMenu : MonoBehaviour
{
    [SerializeField] private Image _rewardImage;

    [SerializeField] private GameObject _finishMenuGameObject;

    [SerializeField] private PlayerStats _playerStats;

    private SceneLoader _sceneLoader;
    
    public UltEvent OnShow;

    [Inject]
    private void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void Exit()
    {
        _sceneLoader.LoadScene(_playerStats.CurrentSceneName);
    }

    public void Restart()
    {
        _sceneLoader.LoadScene(_sceneLoader.GetActiveScene().name);
    }

    public void Show(Sprite rewardImage)
    {
        _rewardImage.sprite = rewardImage;
        OnShow.Invoke();
    }
}