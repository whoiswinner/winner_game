using UnityEngine;
using System.Collections;

public class WallCtrl : MonoBehaviour
{
    

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject != null)
            if (collision.gameObject.tag == "Bullet")
        {
                Debug.Log(gameObject.transform.position);
                Destroy( collision.gameObject);
            
        }
       /* if (gameObject != null)
            if (collision.gameObject.tag == "BigBullet")
            {
                Debug.Log(gameObject.transform.position);
                Destroy(collision.gameObject);

            }
            */

    }
}
