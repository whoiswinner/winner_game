using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pla : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotSpeed = 3.0f;
    public float jumpPower = 3.0f;
    public Camera fpsCam;
    public float cameraLimit;
    public float cameraRotaionX = 0;

    float h;
    float v;

    bool isJumping;

    Rigidbody rigidbody;

    Animator animator;
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        isJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        AnimationUpdate();

        Act();
        MoveCtrl();
        RotCtrl();
        
    }
    void FixedUpdate()
    {
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
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }

    }

    void AnimationUpdate()
    {
        if (h == 0 && v == 0)
        {
            animator.SetBool("isRunning", false);
        
        }
        else
        {
            animator.SetBool("isRunning", true);
        
        }

    }

    void Act()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            animator.SetBool("acttack", true);
        }
        else
        {
            animator.SetBool("acttack", false);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isJumping = true;
        }
    }

}

