using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health_Manager : NetworkBehaviour
{
    public const int maxHealth = 200;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    //public RectTransform healthBar;


    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;
        currentHealth -= amount;

        Debug.Log("Get Damaged!");

    }

    void OnChangedHealth( int health)
    {
        Debug.Log("HEALTH : " + health);
        //healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
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
