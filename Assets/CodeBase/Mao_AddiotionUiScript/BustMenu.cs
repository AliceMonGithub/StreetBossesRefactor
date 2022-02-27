using Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CodeBase.Mao_AddiotionUiScript
{
    public class BustMenu : MonoBehaviour
    {
        //public UltEvent OnShow;
        //public UltEvent OnHide;
        //public UltEvent OnOpenChest;
        //public UltEvent OnFinishOpenChest;

        //[SerializeField] private HorizontalOrVerticalLayoutGroup _horizontalOrVertical;
        //[SerializeField] private PlayerStats _playerStats;

        //[SerializeField] private ChestBust _templateChest;
        //[SerializeField] private Booster[] _boosters;

        //[Min(0)] [SerializeField] private Vector2Int countChestToSpawn;
        //[SerializeField] private LootScreen _lootScreen;

        //[SerializeField] private LootOfChest _template;

        //private List<ChestBust> _chests;
        //private List<Character> _characters;

        //private int _characterCount;

        //public void Show()
        //{
        //    Render();
        //    OnShow.Invoke();
        //}

        //public void Hide() => OnHide.Invoke();

        //private void Render()
        //{
        //    DeleteAllChild(_horizontalOrVertical.transform);

        //    _chests = new List<ChestBust>();

        //    foreach(var booster in _boosters)
        //    {
        //        var instance = Instantiate(_templateChest, _horizontalOrVertical.transform);

        //        instance.Initialize(booster, _playerStats);

        //        _chests.Add(instance);

        //        instance.Opening += OnOpening;
        //    }
        //}

        //private void OnOpening(ChestBust chestBust)
        //{
        //    _characters = chestBust.GetLoot();

        //    _characterCount = _characters.Count;

        //    _lootScreen.Show();
        //    OnOpenChest.Invoke();
        //}

        //private void DeleteAllChild(Transform target)
        //    => target.GetComponentsInChildren<Transform>().Except(new Transform[] { target }).ToList().ForEach(x => Destroy(x.gameObject));

        //public void TryShowNextElement()
        //{
        //    if(_characterCount == 0)
        //    {
        //        _lootScreen.Hide();
        //        OnFinishOpenChest.Invoke();
        //        return;
        //    }

        //    _characterCount--;

        //    var character = _characters[_characterCount];

        //    _lootScreen.SetNewElement(character, _template);
        //}

        //private void OnValidate()
        //{
        //    if (countChestToSpawn.x < 0)
        //        countChestToSpawn.x = 0;
        //    if (countChestToSpawn.y <= countChestToSpawn.x + 1)
        //        countChestToSpawn.y = countChestToSpawn.x + 2;
        //}
    }
}