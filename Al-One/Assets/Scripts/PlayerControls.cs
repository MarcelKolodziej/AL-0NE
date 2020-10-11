using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Config
    [Header("Player Control")]
    [SerializeField] float m_speed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    // State 
    //bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    
    // Message then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        Flip();
        Jump();
    }
    private void Run()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(moveHorizontal * m_speed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);// add velocity on y
            myRigidBody.velocity += jumpVelocityToAdd;

        }
    }
    private void Flip()
        {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            var newScale = new Vector3(
            -1 * Mathf.Abs(this.transform.localScale.x),
             1 * Mathf.Abs(this.transform.localScale.y),
             1 * Mathf.Abs(this.transform.localScale.y));
            this.transform.localScale = newScale;
            myAnimator.SetBool("Running", true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            var newScale = new Vector3(
            1 * Mathf.Abs(this.transform.localScale.x),
            1 * Mathf.Abs(this.transform.localScale.y),
            1 * Mathf.Abs(this.transform.localScale.y));
            this.transform.localScale = newScale;
            myAnimator.SetBool("Running", true);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            myAnimator.SetBool("Running", false);
        }
    }
}
