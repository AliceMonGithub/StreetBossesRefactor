using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ERC20BalanceOfExample : MonoBehaviour
{
    public async Task<BigInteger> GetBalance(string contract)
    {
        string chain = "polygon";
        string network = "testnet";
        string account = PlayerPrefs.GetString("Account");

        BigInteger balanceOf = await ERC20.BalanceOf(chain, network, contract, account);

        return balanceOf;
    }
}
