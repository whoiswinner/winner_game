using UnityEngine;
using System.Collections;

public class WallCtrl : MonoBehaviour
{

    public GameObject sparkEffect;
    void OnCollisionEnter(Collision collision)
    {
        if (gameObject != null)
            if (collision.gameObject.tag == "Bullet")
        {
                SoundManager.instance.PlaySE("Gunwall");
                GameObject spark = Instantiate(sparkEffect, collision.transform.position, Quaternion.identity) as GameObject;
                Destroy(spark, spark.GetComponent<ParticleSystem>().duration + 0.2f);
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
