using UnityEngine;
using System.Collections;

public class BigFireCtrl : MonoBehaviour
{
    public GameObject Bigbullet;
    public Transform firePos;




    void Awake()
    {

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Key_Q();
            BigFire();
        }


    }
    private void Key_Q()
    {
        Debug.Log("Q");
        
    }

    void BigFire()
    {
        CreateBigBullet();
        SoundManager.instance.PlaySE("GunSound11");

    }

    void CreateBigBullet()
    {
        Instantiate(Bigbullet, firePos.transform.position, firePos.transform.rotation);

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
