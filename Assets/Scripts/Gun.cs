using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Gun : MonoBehaviourPunCallbacks
{
    public float damage = 10f;
    public float range = 100f;
    public GameObject gunTip;
     private PlayerControls playerControls;
     private PlayerInput playerInput;
     public GameObject textKillNumObject;
     public TextMeshProUGUI textKillNum;
     public int kills = 0;
     

private void Awake()
    {
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        textKillNumObject = GameObject.Find("KillNum");
        textKillNum = textKillNumObject.GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        if(photonView.IsMine){

        
        if (playerControls.Controls.Shoot.IsPressed())
        {
            //Shoot();
            photonView.RPC("RPC_Shoot", RpcTarget.All);
        }
        }

    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(gunTip.transform.position, gunTip.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    [PunRPC]
    void RPC_Shoot()
    {
        Debug.Log("SHOOTING!");
        RaycastHit hit;
        if(Physics.Raycast(gunTip.transform.position, gunTip.transform.forward, out hit, range))
        {

            // Target target = hit.transform.GetComponent<Target>();
            // if(target != null)
            // {
            //     target.TakeDamage(damage);
            // }

            Health targetHealth = hit.transform.GetComponent<Health>();
            if(targetHealth)
            {
                float enemyHealth = targetHealth.TakeDamage(damage);
                if(enemyHealth <= 0){
                    //YOU KILLED OPPONENT
                    kills++;
                    SetScoreText();
                    enemyHealth = -999;
                }
                Debug.Log("Enemy health: " + targetHealth.health);
            }
        }
    }

    void SetScoreText()
    {
        textKillNum.text = kills.ToString();
    }
}
