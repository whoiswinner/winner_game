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

    private Canvas player_canvas;
    private Canvas player_competition_canvas;

    [SyncVar(hook ="OnCompetitionChanged")] bool Competition;

    void OnCompetitionChanged(bool value)
    {
        if (value == true)
        {

            player_competition_canvas.enabled = true;
            /* if (IsPause == false)
            {
                Time.timeScale = 0;
                IsPause = true;
                return;
            }
            */
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

    }


    void Awake()
    {
        
    }


    public override void OnStartClient()
    {
        Debug.Log("OnStartClient Called!");
        player = GameObject.Find("Local_Player");
        if (player == null)
        {
            Debug.LogError("Local Player Not Detected!");
        }
        PlayerUi = player.GetComponent<PlayerUi>();
        if (PlayerUi == null)
        {
            Debug.LogError("Local Player Not Detected!");
        }
        foreach (Canvas cv in GetComponentsInChildren<Canvas>())
        {
            if (cv.gameObject != gameObject && cv.gameObject.name == "Canvas")
            {
                Debug.Log("I FOUND CANVAS!");
                player_canvas = cv;
            }

            if (cv.gameObject != gameObject && cv.gameObject.name == "Canvas_Competition")
            {
                Debug.Log("I FOUND Competition CANVAS!");
                player_competition_canvas = cv;
            }
        }
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