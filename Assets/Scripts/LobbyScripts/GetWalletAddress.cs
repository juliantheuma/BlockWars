using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoralisWeb3ApiSdk;
using TMPro;

public class GetWalletAddress : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro walletAddressText;

    void Start()
    {
        if(MoralisInterface.IsLoggedIn()){
            Debug.Log(MoralisInterface.GetUser().ethAddress);
            walletAddressText.text = MoralisInterface.GetUser().ethAddress;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
