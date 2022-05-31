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
        public bool EarnMoneyQuest;

        [Space]

        public bool SelectEnemyHelp;
        public bool CanSelectEnemyHelp;
        public bool UseSkillHelp;
        public bool TringleSkillHelp;
        public bool TringleEnemyHelp;
        public bool ManagerTringleHelp;

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

        public void UnlockEarnMoneyQuest()
        {
            EarnMoneyQuest = true;
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
            UpgradeHeroesFirstHelp = true;
        }

        public void UnlockTakeSecondStreetHelp()
        {
            TakeSecondStreetHelp = true;
        }

        public void UnlockSecondCenterAttackHelp()
        {
            SecondCenterAttackHelp = true;
        }

        public void UnlockUpgradeHeroesSecondHelp()
        {
            UpgradeHeroesSecondHelp = true;
        }

        public void UnlockThirdStreetHelp()
        {
            TakeThirdStreetHelp = true;
        }

        public void UnlockSelectEnemyHelp()
        {
            if(CanSelectEnemyHelp)
            {
                SelectEnemyHelp = true;

                CanSelectEnemyHelp = false;
            }
        }
    }
}