using UnityEngine;
using System.Collections;

public class ParticleCtrl : MonoBehaviour
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

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        ParticleShot();
    }



    void ParticleShot()
    {
        rigidbody.AddForce(transform.up * Speed);
    }

}
