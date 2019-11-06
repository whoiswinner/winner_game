using UnityEngine;
using System.Collections;

public class BigFireCtrl : MonoBehaviour
{
    public GameObject Bigbullet;
    public Transform firePos;
    public float time;
    public float stunDeley;
    private PlayerUi PlayerUi;
    private GameObject player;
    private Animator animator;

    private pla pla;

    void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        PlayerUi = player.GetComponent<PlayerUi>();
        animator = player.GetComponent<Animator>();
    }
    void Start()
    {
        stunDeley = 1.0f;
    }
    void Update()
    {
        time += Time.deltaTime;

        if (PlayerUi.skillSlider.value == 100 && Input.GetKeyDown(KeyCode.Q))
        {
            Key_Q();
            BigFire();
            animator.SetBool("Stun", true);
            pla.moveSpeed = 0.0f;

            PlayerUi.currentSkill = 0;
            PlayerUi.skillSlider.value = 0;
            time = 0f;

            StartCoroutine(Stop());


        }

        Debug.Log(time);

    }
    private void Key_Q()
    {
        Debug.Log("Q");

    }

    void BigFire()
    {
        CreateBigBullet();
        SoundManager.instance.PlaySE("GunSound11");

    }
    IEnumerator Stop()
    {

        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Stun", false);
        pla.moveSpeed = 5.0f;


    }
    void CreateBigBullet()
    {
        Instantiate(Bigbullet, firePos.transform.position, firePos.transform.rotation);

    }


    
}
