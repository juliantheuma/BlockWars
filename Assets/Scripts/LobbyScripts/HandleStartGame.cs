using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class HandleStartGame : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if(PhotonNetwork.IsConnected){
            PhotonNetwork.JoinRandomOrCreateRoom();
        }else{
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connected to PUN!");
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to master!");
        PhotonNetwork.JoinRandomOrCreateRoom();
        
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("Disconnected...");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room!");
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
