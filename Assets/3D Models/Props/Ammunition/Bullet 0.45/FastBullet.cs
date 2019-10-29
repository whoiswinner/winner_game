using UnityEngine;
using System.Collections;

public class FastBullet : MonoBehaviour
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
        Fastbulletshot();
    }



    void Fastbulletshot()
    {
        rigidbody.AddForce(transform.up * Speed);
    }
}