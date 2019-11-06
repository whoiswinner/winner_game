using UnityEngine;
using System.Collections;

public class BigBulletcontrol : MonoBehaviour
{
    public int Damage = 20;
    public float Speed = 1000.0f;
    public Rigidbody rigidbody;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }
    void FixedUpdate()
    {
        BigbulletShot();
    }



    void BigbulletShot()
    {
        //rigidbody.AddForce(transform.up * Speed);
    }


    void OnTriggerEnter(Collider collision)
    {
        var hit = collision.gameObject;
        var health_manager = hit.GetComponent<Health_Manager>();


        if (health_manager != null)
        {
            health_manager.TakeDamage(Damage);
            Debug.Log("Remove GameObject Self Because It Hitted Person");
            Destroy(gameObject);
        }

        


        
    }
}