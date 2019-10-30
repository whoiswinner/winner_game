using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{
    public static bool checkSkill;
    public static bool checkShield;
    public static bool ShieldOn;
    public static int dd;

    public static bool IsPause;

    public float deal;

    private PlayerUi PlayerUi;
    private GameObject player;


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

            SceneManager.LoadScene("Soundsc", LoadSceneMode.Additive);
            if (IsPause == false)
            {
                Time.timeScale = 0;
                IsPause = true;
                return;
            }
            Debug.Log(checkSkill);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (checkSkill)
        {
            // Time.timeScale = 1;

            ShieldOn = false;
            if (PlayerUi.currentHealth > 0)
            {
                PlayerUi.TakeDamage((int)deal);
                Destroy(other.gameObject);
                Debug.Log(checkSkill);
                checkSkill = false;


            }
        }
        else if (checkShield)
        {
            Destroy(other.gameObject);
            ShieldOn = false;
            checkShield = false;

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