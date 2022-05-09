using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
    public float health = 200f;
    Renderer [] visuals;
    public bool isAlive = true;
    public int thisPlayerKills = 0;

    public TextMeshProUGUI textKillNum;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
    }

    public float TakeDamage(float damage)
    {
        health -= damage;
        return health;
    }

    IEnumerator Respawn()
    {
        SetRenderers(false);
        health = 200f;
        GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(0,10,0);
        yield return new WaitForSeconds(3);
        GetComponent<CharacterController>().enabled = true;
        SetRenderers(true);
    }

    void SetRenderers(bool state)
    {
        foreach(var renderer in visuals)
        {
            renderer.enabled = state;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        visuals = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if(health <= 0 && isAlive)
        {
            if(photonView.IsMine)
            {
                isAlive = false;
                //run rpc
                photonView.RPC("RPC_PlayerKilled", RpcTarget.All);
                StartCoroutine(Respawn());
            }
        }
    }

    [PunRPC]
    void RPC_PlayerKilled()
    {
        InGameController.playersAlive --;
    }
}
