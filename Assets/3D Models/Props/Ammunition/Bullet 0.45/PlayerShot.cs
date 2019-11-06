using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour
{
    public GameObject Fastbullet;
    public Transform firePos;

    private PlayerUi PlayerUi;
    private GameObject player;

    private float fireDeley = 0.2f;
    private float fireTime;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerUi = player.GetComponent<PlayerUi>();
    }
    void Update()
    {
        fireTime += Time.deltaTime;
        if(fireTime >= 3.0f)
        {
            fireTime = 3.0f;
        }
       
       if (PlayerUi.skillSlider.value >= 20 && Input.GetKeyDown(KeyCode.R))
       {

            StartCoroutine(FastFire());
       
            Key_R();
 
            PlayerUi.currentSkill -= 20;
            PlayerUi.skillSlider.value -= 20;

       }


       


    }
    private void Key_R()
    {
        Debug.Log("R");

    }

    IEnumerator FastFire()
    {
        for(int i = 0; i<3; i++)
        {
            CreateFastBullet();
            SoundManager.instance.PlaySE("GunSound");
            yield return new WaitForSeconds(fireDeley);
        }
        
    }


    void FastFire1()
    {
        CreateFastBullet();
        

    }

    void CreateFastBullet()
    {
        Instantiate(Fastbullet, firePos.transform.position, firePos.transform.rotation);

    }


    /* void CreateBullet()
     {
         GameObject player = GameObject.FindWithTag("Player");
         // player.transform.eulerAngles.x
         Quaternion delta_rotated = player.transform.rotation.eulerAngles;
         delta_rotated.eulerAngles
         Vector3 delta_pos = player.transform.position;

         //delta_rotated.x = 90;
         Debug.Log("Changed Rot :" + delta_rotated.eulerAngles);
         Instantiate(bullet, delta_pos, delta_rotated);
     }

     */
}
