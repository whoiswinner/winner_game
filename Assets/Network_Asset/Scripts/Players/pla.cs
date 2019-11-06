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

    public GameObject bigBulletPrefab;
    public Transform bigBulletSpawn;

    public GameObject fastBulletPrefab;
    public Transform fastBulletSpawn;

    public GameObject shield;
    public ParticleSystem particles;

    private NetworkAnimator netAnimator;


    private Camera cam;
    private AudioListener aud;
    private PlayerUi PlayerUi;

    private float fireDeley = 0.98f;
    private float fireTime;

    float h;
    float v;

    bool isJumping;

    Rigidbody rigidbody;
    public GameObject sparkEffect;
    Animator animator;


    //Shield

    private Canvas player_canvas;
    private Canvas player_competition_canvas;

    public int Decibel;

    private Health_Manager hm;


    public Transform firePos;

    bool ShieldOn;


    [SyncVar(hook = "OnCompetitionChanged")] bool Competition;

    void OnCompetitionChanged(bool value)
    {
        if (Competition)
        {
            player_competition_canvas.GetComponentInChildren<BScore>().Init();
            player_competition_canvas.GetComponentInChildren<FScore>().Init();
            player_competition_canvas.enabled = true;
        }
        else
        {
            player_competition_canvas.enabled = false;
        }
    }

    public void Receive_Decibel(int db)
    {
        Decibel = db;
    }

    [ClientRpc]
    void RpcSyncDBWithClients(int value)
    {
        Decibel = value;
    }

    [Command]
    void CmdDB(int value)
    {
        Debug.Log("CmdDB " + value);
        RpcChangeDB(value);
    }

    [ClientRpc]
    void RpcChangeDB(int value)
    {

        Debug.Log("RPC Change RpcChangeDB" + value);
        Decibel = value;
    }



    [ClientRpc]
    void RpcSyncVarWithClients(bool varToSync)
    {
        Competition = varToSync;
    }

    [Command]
    void CmdComPetition(bool OnOff)
    {
        Debug.Log("CmdCompetition " + OnOff);
        RpcChangeCompetition(OnOff);
    }

    [ClientRpc]
    void RpcChangeCompetition(bool OnOff)
    {

        Debug.Log("RPC Change Competition" + OnOff);
        Competition = OnOff;
    }

    public void Finish_Competition(int winner)
    {
        if(winner == 0)
        {
            hm.AddHealth(10);
            
        }
        else
        {
            hm.TakeDamage(50);
        }
        Competition = false;
        CmdComPetition(Competition);
    }




    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        gameObject.tag = "Player";
        gameObject.name = "Local_Player";
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
        aud = GetComponentInChildren<AudioListener>();
        hm = GetComponentInChildren<Health_Manager>();
        
        foreach (Canvas cv in gameObject.GetComponentsInChildren<Canvas>())
        {
            print("cv Found! " + cv.gameObject.name);
            if (cv.gameObject != gameObject && cv.gameObject.name == "Canvas")
            {
                player_canvas = cv;
            }

            if (cv.gameObject != gameObject && cv.gameObject.name == "Canvas_Competition")
            {
                player_competition_canvas = cv;
                player_competition_canvas.enabled = false;
            }
        }
        netAnimator = GetComponent<NetworkAnimator>();
        PlayerUi = GetComponent<PlayerUi>();
    }

    

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isLocalPlayer);
        if (!isLocalPlayer)
        {
            if (cam.enabled)
                cam.enabled = false;
            if (aud.enabled)
                aud.enabled = false;
            if (player_canvas.enabled)
                player_canvas.enabled = false;
            return;
        }

        if (!cam.enabled)
            cam.enabled = true;

        if (!aud.enabled)
            aud.enabled = true;

        if (!player_canvas.enabled)
            player_canvas.enabled = true;



        


        fireTime += Time.deltaTime;

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

        if (!Competition)
        {
            Act();
            MoveCtrl();
        }
        
        //RotCtrl();
        
    }


    void FixedUpdate()
    {
        
        if (!isLocalPlayer)
        {
            return;
        }
        //Debug.Log("Fixed Upate");
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

    public void Act(int action = 0)
    {
        if(action != 0)
        {
            Debug.LogError("ACTION FOUND!!!!!!!!!!!!!!!! :: " + action);
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (fireTime >= fireDeley)
            {
                CmdFire();
                fireTime = 0;
            }
            netAnimator.animator.SetBool("acttack", true);

        }

        else if (PlayerUi.skillSlider.value >= 100 && (Input.GetKeyDown(KeyCode.Q) || action == 1)) {
            CmdBigFire();
            netAnimator.animator.SetBool("acttack", true);
            PlayerUi.currentSkill -= 100;
            PlayerUi.skillSlider.value -= 100;
        }
        else if (PlayerUi.skillSlider.value >= 20 && (Input.GetKeyDown(KeyCode.R) || action == 3))
        {
            CmdFastFire();
            netAnimator.animator.SetBool("acttack", true);
            PlayerUi.currentSkill -= 20;
            PlayerUi.skillSlider.value -= 20;
        }
        else if (PlayerUi.shieldSlider.value == 100 && (Input.GetKeyDown(KeyCode.E) || action == 2))
        {
            CmdShield();
            PlayerUi.currentShield = 0;
            PlayerUi.shieldSlider.value = 0;

        }
        else
        {
            netAnimator.animator.SetBool("acttack", false);
        }
    }

    IEnumerator Stop()
    {

        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Stun", false);
        pla.moveSpeed = 5.0f;
    }

    [Command]
    void CmdFire()
    {
        Debug.Log("FIRE!");

        SoundManager.instance.PlaySE("GunSound");

        Debug.Log("CmdFire!");
        Debug.Log(bulletSpawn.position);
        Debug.Log(bulletSpawn.transform);

        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward;

        NetworkServer.Spawn(bullet);

        
        
        Destroy(bullet, 10.0f);
    }

    [Command]
    void CmdBigFire()
    {
        var bigBullet = (GameObject)Instantiate(bigBulletPrefab, bigBulletSpawn.position, bigBulletSpawn.rotation);

        NetworkServer.Spawn(bigBullet);

        Destroy(bigBullet, 10.0f);


        SoundManager.instance.PlaySE("GunSound11");

        netAnimator.animator.SetBool("Stun", true);
        pla.moveSpeed = 0.0f;
        StartCoroutine(Stop());
    }


    [Command]
    void CmdFastFire()
    {
        Debug.Log("cmdFastFire");
        StartCoroutine(FastFire());

    }

    IEnumerator FastFire()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Fast Fire Shoot");
            var fastBullet = (GameObject)Instantiate(fastBulletPrefab, fastBulletSpawn.position, fastBulletSpawn.rotation);

            NetworkServer.Spawn(fastBullet);

            Destroy(fastBullet, 10.0f);
            SoundManager.instance.PlaySE("GunSound");
            yield return new WaitForSeconds(fireDeley);
        }

    }

    [Command]
    void CmdShield()
    {
        particles.Play();
        Instantiate(shield, gameObject.transform.position, gameObject.transform.rotation);
        SoundManager.instance.PlaySE("Shield");
        ShieldOn = true;
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
            //GameObject spark = Instantiate(sparkEffect, collision.transform.position, Quaternion.identity) as GameObject;
            Debug.Log("You Are HitteD!!!");
            Destroy(collision.gameObject);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BigBullet" && ShieldOn)
        {
            Debug.Log("Open Competition Now!");
            Competition = true;
            CmdComPetition(Competition);
        }
    }

}

