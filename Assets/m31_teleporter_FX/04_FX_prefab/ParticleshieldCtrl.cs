using UnityEngine;
using System.Collections;

public class ParticleshieldCtrl : MonoBehaviour
{
    public GameObject shield;
    public Transform firePos;

    public ParticleSystem particles;



    void Awake()
    {

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Key_E();
            shieldCtrl();
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
