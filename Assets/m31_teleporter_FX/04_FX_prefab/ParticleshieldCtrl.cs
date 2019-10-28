using UnityEngine;
using System.Collections;

public class ParticleshieldCtrl : MonoBehaviour
{
    public GameObject shield;
    public Transform firePos;

    public ParticleSystem particles;

    private PlayerUi PlayerUi;
    private GameObject player;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerUi = player.GetComponent<PlayerUi>();
    }
    void Update()
    {
        if (PlayerUi.shieldSlider.value == 100 && Input.GetKey(KeyCode.E))
        {
            Key_E();
            shieldCtrl();
            PlayerUi.currentShield = 0;
            PlayerUi.shieldSlider.value = 0;
        }

    }
    private void Key_E()
    {
        particles.Play();
        Debug.Log("E");

    }

    void shieldCtrl()
    {
        Createshield();
        SoundManager.instance.PlaySE("Shield");

    }

    void Createshield()
    {
        Instantiate(shield, firePos.transform.position, firePos.transform.rotation);

    }


    /* void CreateBullet()
     {
         GameObject player = GameObject.FindWithTag("Player");
         // player.transform.eulerAngles.x
         Quaternion delta_rotated = player.transform.rotation.eulerAngles;
         delta_rotated.eulerAngles
         Vector3 delta_pos = player.transform.position;

         //delta_rotated.x = 90;
         Debug.Log("Changed Rot :" + delta_rotated.eulerAngles);
         Instantiate(bullet, delta_pos, delta_rotated);
     }

     */
}
