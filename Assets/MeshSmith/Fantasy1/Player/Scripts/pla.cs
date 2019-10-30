using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class pla : NetworkBehaviour
{
    
    public static float moveSpeed = 5.0f;
    public float rotSpeed = 3.0f;
    public float jumpPower = 3.0f;
    public Camera fpsCam;
    public float cameraLimit;
    public float cameraRotaionX = 0;


    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private NetworkAnimator netAnimator;


    private Camera cam;

    float h;
    float v;

    bool isJumping;

    Rigidbody rigidbody;
    public GameObject sparkEffect;
    Animator animator;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        gameObject.tag = "Player";
        Debug.LogError("IM LOCAL PLAYER!");
    }


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        isJumping = true;
        netAnimator = GetComponent<NetworkAnimator>();
    }

    

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isLocalPlayer);
        if (!isLocalPlayer)
        {
            if (cam.enabled)
                cam.enabled = false;
            return;
        }

        if (!cam.enabled)
            cam.enabled = true;


        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * rotSpeed;
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(_characterRotationY));

        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * rotSpeed;
        cameraRotaionX -= _cameraRotationX;
        cameraRotaionX = Mathf.Clamp(cameraRotaionX, -cameraLimit, cameraLimit);

        fpsCam.transform.localEulerAngles = new Vector3(cameraRotaionX, 0f, 0f);

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        AnimationUpdate();

        Act();
        MoveCtrl();
        //RotCtrl();
        
    }


    void FixedUpdate()
    {
        
        if (!isLocalPlayer)
        {
            return;
        }
        Debug.Log("Fixed Upate");
        Jump();
    }

    void MoveCtrl()
    {
        if (Input.GetKey(KeyCode.W))
        {
         
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        
        }

    }

    /*
    void RotCtrl()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * rotSpeed;
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(_characterRotationY));

        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * rotSpeed;
        cameraRotaionX -= _cameraRotationX;
        cameraRotaionX = Mathf.Clamp(cameraRotaionX, -cameraLimit, cameraLimit);

        fpsCam.transform.localEulerAngles = new Vector3(cameraRotaionX, 0f, 0f);
    }
    */

    void Jump()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            
            if (isJumping)
            {
                Debug.Log("점프가능");
                isJumping = false;
                rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                SoundManager.instance.PlaySE("Jump1");
            }
            else
            {
                Debug.Log("점프불가능");
                return;
            }
            netAnimator.animator.SetBool("Jumping", true);
        }
        else
        {
            netAnimator.animator.SetBool("Jumping", false);
        }

    }

    void AnimationUpdate()
    {
        if (h == 0 && v == 0)
        {
            netAnimator.animator.SetBool("isRunning", false);
        
        }
        else
        {
            netAnimator.animator.SetBool("isRunning", true);
        
        }

    }
    
    void Act()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CmdFire();
            netAnimator.animator.SetBool("acttack", true);
        }
        else
        {
            netAnimator.animator.SetBool("acttack", false);
        }
    }


    void CmdFire()
    {
        Debug.Log("FIRE!");

        SoundManager.instance.PlaySE("GunSound");

        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 20.0f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isJumping = true;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            SoundManager.instance.PlaySE("Gunwall");
            GameObject spark = Instantiate(sparkEffect, collision.transform.position, Quaternion.identity) as GameObject;
            Destroy(collision.gameObject);
        }
    }

}

