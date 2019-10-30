using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
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
            health_manager.TakeDamage(10);
        }

        Debug.Log("Remove GameObject Self");


        Destroy(gameObject);
    }
}
