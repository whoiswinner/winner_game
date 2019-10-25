using UnityEngine;
using System.Collections;

public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;


    

    void Awake()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
            
        }
      

    }

    void Fire()
    {
        CreateBullet();

    }

    void CreateBullet()
    {
        Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);

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
