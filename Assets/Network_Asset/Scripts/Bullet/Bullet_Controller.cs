using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{

    public int Damage = 10;
    public float Speed = 500.0f;
    public Rigidbody rigidbody;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        rigidbody.AddForce(transform.up * Speed);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var health_manager = hit.GetComponent<Health_Manager>();


        if(health_manager != null)
        {
            health_manager.TakeDamage(Damage);
        }

        Debug.Log("Remove GameObject Self");


        Destroy(gameObject);
    }
}
