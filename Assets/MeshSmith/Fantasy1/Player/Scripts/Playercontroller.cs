using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{

    public float speed = 2f;
    public float jumpPower = 5f;
    public float rotateSpeed = 4f;

    float horizontalMove;
    float verticalMove;

    Rigdbody rigdbody;
    Animator animator;

    Vector3 movenment;
    bool IsJumping;

    void Awake()
    {
        rigdbody = GetComponent<Rigdbody>();
        animator = GetComponent<Animator>();
    }

    void Updata()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
            IsJumping = true;

        AnimationUpdate();
    }

    void FixedUpdate()
    {
        jumpPower();
        Run();
        Turn();
    }

    void AnimationUpdata()
    {
        if (horizontalMove == 0 && verticalMove == 0)
        {
            animator.SetBool("IsRunning", false);
        }
        else
            animator.SetBool("IsRunning", true);
    }
    void Run()
    {
        movement.Set(horizontalMove, 0, verticalMove);
        movenment = movenment.normalized * speed * Time.deltaTime;
        rigdbody.MovePosition(transform.position + movenment);
    }

    void Turn()
    {
        if (horizontalMove == 0 && verticalMove == 0)
            return;

        Quaternion newRotation = Quaternion.LookRotation(movement);

        rigdbody.rotation = Quaternion.Slerp(rigdbody.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (!IsJumping)
            return;

        rigdbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);

        IsJumping = false;
    }

    void start()
    {

    }