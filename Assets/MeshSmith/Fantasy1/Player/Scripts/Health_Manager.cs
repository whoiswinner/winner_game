using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Manager : MonoBehaviour
{
    public const int maxHealth = 200;
    public int currentHealth = maxHealth;


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        Debug.Log("Get Damaged!");
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
