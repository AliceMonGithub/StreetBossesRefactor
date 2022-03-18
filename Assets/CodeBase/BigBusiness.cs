using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase
{
    public class BigBusiness : MonoBehaviour
    {
        public List<BusinessUpgradeIcon> Icons { get; set; } = new List<BusinessUpgradeIcon>();

        public void ShowNext(int nextIndex)
        {
            var icon = Icons.Find(icon => icon.Business.Index == nextIndex);

            if (icon == null || icon.gameObject.activeSelf == true) return;

            icon.gameObject.SetActive(true);

            icon.RefreshBigBusiness();
        }
    }
}