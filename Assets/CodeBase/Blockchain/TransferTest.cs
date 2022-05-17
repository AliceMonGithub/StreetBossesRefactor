using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Blockchain
{
    public class TransferTest : MonoBehaviour
    {
        [SerializeField] private string amount;

        [SerializeField] private Web3WalletTransfer20Example _transfer;

        private void Start()
        {
            _transfer.OnTransfer20(amount);
        }
    }
}