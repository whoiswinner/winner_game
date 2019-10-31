using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class SceneChange : NetworkBehaviour
{
    public static bool checkSkill;
    public static bool checkShield;
    public static bool ShieldOn;
    public static int dd;

    public static bool IsPause;

    public float deal;

    private PlayerUi PlayerUi;
    private GameObject player;

    [SyncVar] bool Competition;


    [ClientRpc]
    void RpcSyncVarWithClients(bool varToSync)
    {
        Competition = varToSync;
    }

    [Command]
    void CmdComPetition(bool OnOff)
    {
        Debug.Log("CmdCompetition " + OnOff);
        RpcChangeCompetition(OnOff);
    }

    [ClientRpc]
    void RpcChangeCompetition(bool OnOff)
    {
        
        Debug.Log("RPC Change Competition" + OnOff);
        Competition = OnOff;

        if (OnOff == true)
        {

            SceneManager.LoadScene("Soundsc", LoadSceneMode.Additive);
            if (IsPause == false)
            {
                Time.timeScale = 0;
                IsPause = true;
                return;
            }
            Debug.Log(checkSkill);
        }
        else
        {

            if (checkSkill)
            {
                // Time.timeScale = 1;

                ShieldOn = false;
                if (PlayerUi.currentHealth > 0)
                {
                    PlayerUi.TakeDamage((int)deal);
                    Debug.Log(checkSkill);
                    checkSkill = false;


                }
            }
            else if (checkShield)
            {
                
                ShieldOn = false;
                checkShield = false;

            }
        }



    }


    void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        PlayerUi = player.GetComponent<PlayerUi>();
    }
    void Start()
    {

        deal = 50.0f;
    }
    void Update()
    {
        stop();
    }

    void OnTriggerEnter(Collider BigBullet)
    {

        if (ShieldOn && BigBullet.gameObject.tag == "BigBullet")
        {
            Competition = true;
            CmdComPetition(Competition);
            Debug.Log("Competition : " + Competition);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "BigBullet")
        {
            Competition = false;
            CmdComPetition(Competition);
            Debug.Log("Competition : " + Competition);
            Destroy(other.gameObject);
        }
        
    }

    void stop()
    {
        if (checkSkill || checkShield)
        {
            if (IsPause == true)
            {
                Time.timeScale = 1;
                IsPause = false;
                return;
            }

        }


    }

    void skillAndShield()
    {

    }
}