using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour
{
    public int Damage = 20;
    public float Speed = 1000.0f;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * Speed);
    }

  
}
