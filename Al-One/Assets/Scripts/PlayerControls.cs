using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Config
    [Header("Player Control")]
    [SerializeField] float m_speed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float m_verticalSpeed = 10f;
    float gravityScaleAtStart;
    // State 
    //bool isAlive = true

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    BoxCollider2D myCollider;
    [SerializeField] private LayerMask platformLayerMask;
    // Message then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;

    }

    void Update()
    {
        Run();
        ClimbLadder();
        Jump();
        Flip();
    }
    private void Run()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(moveHorizontal * m_speed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    private void Jump()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private bool IsGrounded()
    {
        float extraHeightText = 0.5f;
        RaycastHit2D rayCastHit = Physics2D.BoxCast(myCollider.bounds.center, myCollider.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        Color rayColor;
        if (rayCastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(myCollider.bounds.center + new Vector3(myCollider.bounds.extents.x, 0), Vector2.down * (myCollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(myCollider.bounds.center - new Vector3(myCollider.bounds.extents.x, 0), Vector2.down * (myCollider.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(myCollider.bounds.center - new Vector3(myCollider.bounds.extents.x, myCollider.bounds.extents.y + extraHeightText), Vector2.right * (myCollider.bounds.extents.x * 2f), rayColor);
        // Debug.Log(rayCastHit.collider);
        return rayCastHit.collider != null;
    }
    private void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            var newScale = new Vector3(
            1 * Mathf.Abs(this.transform.localScale.x),
             1 * Mathf.Abs(this.transform.localScale.y),
             1 * Mathf.Abs(this.transform.localScale.y));
            this.transform.localScale = newScale;
            myAnimator.SetBool("Running", true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            var newScale = new Vector3(
            -1 * Mathf.Abs(this.transform.localScale.x),
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
    private void ClimbLadder()
    {
        float moveVertical = Input.GetAxis("Vertical");

        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) || (IsGrounded() && moveVertical == 0f))
        {
            myAnimator.SetBool("Climbing", false);
            myAnimator.SetBool("Climbing_Idle", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        Vector2 playerClimbingVelocity = new Vector2(myRigidBody.velocity.x, moveVertical * m_verticalSpeed);
        myRigidBody.velocity = playerClimbingVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;

        if (playerHasVerticalSpeed)
        {
            myAnimator.SetBool("Climbing", true);
            myAnimator.SetBool("Climbing_Idle", false);
        }
        else
        {
            myAnimator.SetBool("Climbing", true);
            myAnimator.SetBool("Climbing_Idle", true);
        }

        myRigidBody.gravityScale = 0f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Platform"))
        {
            this.transform.parent = col.transform;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Platform"))
        {
            this.transform.parent = null;
        }
    }
}