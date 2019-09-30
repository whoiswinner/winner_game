using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float speed = 10f;

    Rigidbody rigidbody;
    Vector3 movement;
    float h, v;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + movement);
    }

}