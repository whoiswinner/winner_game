using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour
{
    public int Damage = 20;
    public float Speed = 1000.0f;
    public Rigidbody rigidbody;

    private PlayerUi PlayerUi;
    private GameObject player;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerUi = player.GetComponent<PlayerUi>();
    }
    void Start()
    {

    }
    void FixedUpdate()
    {
        bulletShot();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject != null)
            if (collision.gameObject.tag == "Player")
            {
                if (PlayerUi.currentHealth > 0)
                {
                    PlayerUi.TakeDamage(Damage);

                }



            }
    }

    void bulletShot()
    {
        rigidbody.AddForce(transform.up * Speed);
    }
}
