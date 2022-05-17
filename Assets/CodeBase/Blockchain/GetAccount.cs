using TMPro;
using UnityEngine;

namespace Assets.CodeBase.Blockchain
{
    public class GetAccount : MonoBehaviour
    {
        [SerializeField] private TMP_Text _accountText;

        private void Awake()
        {
            var account = PlayerPrefs.GetString("Account");

            Debug.LogWarning("GetAccount");

            Debug.LogWarning(account);

            _accountText.text = account.ToString();
        }
    }
}