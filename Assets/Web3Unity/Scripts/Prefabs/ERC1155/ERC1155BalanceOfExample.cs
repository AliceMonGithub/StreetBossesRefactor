using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ERC1155BalanceOfExample : MonoBehaviour
{
    public bool Test;
    public string TestToken;

    private async void Awake()
    {
        if(Test)
        {
            await CheckBalance(TestToken);
        }
    }

    public async Task<bool> CheckBalance(string tokenID)
    {
        string chain = "polygon";
        string network = "testnet";
        string contract = "0x2953399124F0cBB46d2CbACD8A89cF0599974963";
        string account = PlayerPrefs.GetString("Account");

        Debug.LogWarning("PRINTING BALANCE:");

        BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenID);

        if(balanceOf != null)
        {
            Debug.LogWarning(balanceOf > 0);
        }
        else
        {
            Debug.LogWarning("false");
        }

        if(balanceOf != null && balanceOf > 0)
        {
            return true;
        }

        return false;
    }

    public async Task<BigInteger> GetBalance(string tokenID, string contract = "0x2953399124F0cBB46d2CbACD8A89cF0599974963")
    {
        string chain = "polygon";
        string network = "testnet";
        string account = PlayerPrefs.GetString("Account");

        Debug.LogWarning("PRINTING BALANCE:");

        BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenID);

        Debug.LogWarning(balanceOf);

        return balanceOf;
    }
}
