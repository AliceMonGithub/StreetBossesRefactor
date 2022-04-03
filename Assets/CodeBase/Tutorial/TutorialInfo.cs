using System.Collections;
using UniRx;
using UnityEngine;

namespace Assets.CodeBase
{
    [CreateAssetMenu()]
    public class TutorialInfo : ScriptableObject
    {
        public bool StartupHelp;
        public bool BusinessHelp;
        public bool BusinessTakenHelp;
        public bool EarnMoneyHelp;
        public bool SetManagerHelp;
        public bool GoNextStreetHelp;
        public bool SecondStreetAttackHelp;
        public bool UpgradeHeroesFirstHelp;
        public bool TakeSecondStreetHelp;
        public bool SecondCenterAttackHelp;
        public bool UpgradeHeroesSecondHelp;
        public bool TakeThirdStreetHelp;

        public void UnlockBusinessHelp()
        {
            BusinessHelp = true;
        }

        public void UnlockBusinessTakenHelp()
        {
            BusinessTakenHelp = true;
        }

        public void UnlockEarnMoneyHelp()
        {
            EarnMoneyHelp = true;
        }

        public void UnlockSetManagerHelp()
        {
            SetManagerHelp = true;
        }

        public void UnlockGoNextStreetHelp()
        {
            GoNextStreetHelp = true;
        }

        public void UnlockSecondStreetAttackHelp()
        {
            SecondStreetAttackHelp = true;
        }

        public void UnlockUpgradeHeroesFirstHelp()
        {
            SecondStreetAttackHelp = true;
        }

        public void UnlockTakeSecondStreetHelp()
        {
            SecondStreetAttackHelp = true;
        }

        public void UnlockSecondCenterAttackHelp()
        {
            SecondStreetAttackHelp = true;
        }

        public void UnlockUpgradeHeroesSecondHelp()
        {
            SecondStreetAttackHelp = true;
        }

        public void UnlockThirdStreetHelp()
        {
            SecondStreetAttackHelp = true;
        }
    }
}