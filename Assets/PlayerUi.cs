using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUi : MonoBehaviour
{

    public int startHealth = 100;
    public int startShield = 0;
    public int startSkill = 0;

    public int currentHealth;
    public int currentShield;
    public int currentSkill;

    public Slider healtSlider;
    public Slider shieldSlider;
    public Slider skillSlider;

    private Animator anim;

    private pla pla;

    private bool isDead;

    private bool damaged;


    void Awake()
    {
        anim = GetComponent<Animator>();
        pla = GetComponent<pla>();

        currentHealth = startHealth;
        currentShield = startShield;
        currentSkill = startSkill;
    }



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Skill", 0, 1);
        InvokeRepeating("Shield", 0, 1);
        damaged = false;



    }

    // Update is called once per frame
    void Update()
    {
        damaged = false;
        anim.SetBool("damaged", false);


    }
    public void TakeDamage(int amount)
    {
        damaged = true;
        anim.SetBool("damaged", true);
        currentHealth -= amount;
        healtSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }
    void Damaged()
    {



    }

    void Skill()
    {
        if (currentSkill <= 100)
        {
            currentSkill += 10;
            skillSlider.value = currentSkill;

        }

    }

    void Shield()
    {
        if (currentShield <= 100)
        {
            currentShield += 10;
            shieldSlider.value = currentShield;

        }
    }
    void Death()
    {
        isDead = true;

        anim.SetTrigger("Die");

        pla.enabled = false;
    }
}
