using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Health_Manager : NetworkBehaviour
{
    public const int maxHealth = 200;

    [SyncVar(hook = "OnChangeHealth")]
    float currentHealth;

    public Slider hpBar;


    void OnChangeHealth(float currentHealth)
    {
        Debug.Log("Changed Health : " + currentHealth);
        hpBar.value = currentHealth;
    }


    [ServerCallback]
    public void TakeDamage(int amount)
    {
        if (!isServer) return;

        Debug.Log("Get Damage!" + amount);

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    [ServerCallback]
    public void AddHealth(int amount)
    {
        if (!isServer) return;

        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


    public override void OnStartClient()
    {
        base.OnStartClient();
        currentHealth = 200;
    }

}
