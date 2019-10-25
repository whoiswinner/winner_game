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
        rigidbody.AddForce(transform.up * Speed);
    }
}