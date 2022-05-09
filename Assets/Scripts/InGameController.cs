using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Cinemachine;

using UnityEngine.UI;
using TMPro;

public class InGameController : MonoBehaviourPunCallbacks
{
    public CinemachineVirtualCamera myVcam;
    public bool isPaused = false;
    public GameObject pauseCanvas;
    public static InGameController instance;

    public static int killNum = 0;
    public static int playersAlive = 20;
    public TextMeshProUGUI textPlayersAlive;

    // Start is called before the first frame update
    void Start()
    {
        GameObject myPlayer = PhotonNetwork.Instantiate("Player 1", new Vector3(0,2,0), Quaternion.identity);
        myVcam.Follow = myPlayer.GetComponent<Transform>();
        myVcam.LookAt = myPlayer.GetComponent<Transform>();
        SetPaused();
        
    }

    private void Awake()
    {
        instance = this;
        SetScoreText();
    }

    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LeaveLobby();

        PhotonNetwork.LoadLevel(2);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            SetPaused();
        }
        SetScoreText();
    }

    void SetPaused()
    {
        //set canvas
        pauseCanvas.SetActive(isPaused);
    }

    void SetScoreText()
    {
        textPlayersAlive.text = playersAlive.ToString();
    }
}
