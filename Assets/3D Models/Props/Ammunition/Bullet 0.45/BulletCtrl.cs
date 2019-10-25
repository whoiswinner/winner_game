using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour
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
        bulletShot();
    }



    void bulletShot()
    {
        rigidbody.AddForce(transform.up * Speed);
    }
}
