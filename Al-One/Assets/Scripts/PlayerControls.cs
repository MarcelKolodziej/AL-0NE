using Cinemachine;
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

    // Death
    private bool PlayerHasControl = true;
    [SerializeField] private float ForceModifier = 1f;
    private IEnumerator coroutine;

    //Sound
    [SerializeField] private SoundPlayer SoundPlayer;

    //UI
    [SerializeField] private HUDController HUDController;

    //Jet-Pack
    [SerializeField] private ParticleSystem JetpackParticles;
    private ParticleSystem.ShapeModule JetpackParticleShape;
    [Space]
    [SerializeField] private bool JetPackEnabled = false;
    [SerializeField] private float jetPackForce = 3f;
    [SerializeField] private float JetpackFuel = 1f;
    [SerializeField] private float FuelDecaySpeed = 0.0f;
    [Space]
    [SerializeField] private Vector3 ParticleLeftVector;
    [SerializeField] private Vector3 ParticleDownVector;
    [SerializeField] private Vector3 ParticleRightVector;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCollider;
    [SerializeField] private ParticleSystem BloodParticles;
    [SerializeField] private ParticleSystem FireParticles;
    [SerializeField] private ParticleSystem AcidParticles;
    [SerializeField] private LayerMask platformLayerMask;
    private GameObject myCineMachineCamera;
    private CinemachineStateDrivenCamera CinemachineStateDrivenCamera;

    //Optimization
    private bool isGrounded = false;

    //Easter Egg
    private int keyIndex;
    private KeyCode[] konamicode = new KeyCode[] {KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow,
    KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A};

    // Message then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myCineMachineCamera = GameObject.FindGameObjectWithTag("CMVirtualCam");
        CinemachineStateDrivenCamera = myCineMachineCamera.GetComponent<CinemachineStateDrivenCamera>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        JetpackParticleShape = JetpackParticles.shape;
    }

    void Update()
    {
        isGrounded = IsGrounded();

        if (PlayerHasControl)
        {
            Run();
            ClimbLadder();
            Jump();
            Flip();
        }

        KonamiCode();
    }

    /// <summary>
    /// My own little easter egg to activate the jetpack using the KonamiCode
    /// </summary>
    private void KonamiCode()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(konamicode[keyIndex]))
            {
                keyIndex++;
            }
            else
            {
                keyIndex = 0;
            }

            if (keyIndex == konamicode.Length)
            {
                keyIndex = 0;
                HUDController.SetJetpackGUIOn();
                JetPackEnabled = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (JetPackEnabled)
        {
            if (PlayerHasControl)
            {
                JetPackFly();
            }
            else
            {
                JetpackParticles.Play();
                SoundPlayer.PlayJetpackSound(true);
                myRigidBody.AddForce(new Vector2(0, jetPackForce));
                JetpackFuel -= FuelDecaySpeed;
                HUDController.JetpackFuelDisplay.fillAmount = JetpackFuel;
            }
        }
    }

    private void JetPackFly()
    {
        if (Input.GetKey(KeyCode.F) && JetpackFuel > 0)
        {
            JetpackParticles.Play();
            SoundPlayer.PlayJetpackSound();
            float moveHorizontal = Input.GetAxis("Horizontal");

            if (moveHorizontal < 0)
            {
                JetpackParticleShape.rotation = ParticleRightVector;
            }
            else if (moveHorizontal > 0) 
            {
                JetpackParticleShape.rotation = ParticleLeftVector;
            }
            else
            {
                JetpackParticleShape.rotation = ParticleDownVector;
            }

            myRigidBody.AddForce(new Vector2(0, jetPackForce));
            JetpackFuel -= FuelDecaySpeed;
            HUDController.JetpackFuelDisplay.fillAmount = JetpackFuel;
        }
        else
        {
            SoundPlayer.StopJetpackSound();
            JetpackParticles.Stop();
        }

        if (isGrounded)
        {
            JetpackFuel = 1f;
            HUDController.JetpackFuelDisplay.fillAmount = JetpackFuel;
        }
    }

    private void Run()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(moveHorizontal * m_speed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        if (moveHorizontal != 0 && isGrounded)
        {
            SoundPlayer.StartWalkingSFX();
        }
        else
        {
            SoundPlayer.StopWalkingSFX();
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            SoundPlayer.PlayJumpingSFX();
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

        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) || (isGrounded && moveVertical == 0f))
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

        if (col.gameObject.tag == "DamageBlock" && coroutine == null)
        {
            PlayerHasControl = false;
            myRigidBody.freezeRotation = false;
            myAnimator.SetBool("Dead", true);
            SelectParticles(col.gameObject.name);
            myRigidBody.AddForceAtPosition(GenerateRandomForce(), col.gameObject.transform.position, ForceMode2D.Impulse);
            coroutine = PlayerDeathSequence();
            StartCoroutine(coroutine);
        }

        if (coroutine != null)
        {
            myRigidBody.AddForceAtPosition(GenerateRandomForce(), col.gameObject.transform.position, ForceMode2D.Impulse);
            SelectParticles(col.gameObject.name);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DamageBlock" && coroutine == null)
        {
            PlayerHasControl = false;
            myRigidBody.freezeRotation = false;
            myAnimator.SetBool("Dead", true);
            coroutine = PlayerDeathSequence();
            StartCoroutine(coroutine);
        }
    }

    public void SelectParticles(string collisionObjectName)
    {
        switch (collisionObjectName)
        {
            case "Foreground Damage Lava":
                FireParticles.Play();
            break;

            case "Foreground Damage Acid":
                AcidParticles.Play();
                break;
            
            case "Foreground Damage Spikes":
            default:
                BloodParticles.Play();
                break;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Platform"))
        {
            this.transform.parent = null;
        }
    }

    private IEnumerator PlayerDeathSequence()
    {
        yield return new WaitForSeconds(1.5f);

        // Clean Up and Reset Code
        myAnimator.SetBool("Dead", false);
        PlayerHasControl = true;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        myRigidBody.freezeRotation = true;
        myRigidBody.velocity = Vector2.zero;

        BloodParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        FireParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        AcidParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        JetpackParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        SoundPlayer.StopJetpackSound();

        Vector3 oldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameManager.Instance.TriggerPlayerDeath();
        CinemachineStateDrivenCamera.PreviousStateIsValid = false;
        CinemachineStateDrivenCamera.OnTargetObjectWarped(transform, transform.position - oldPosition);
        JetpackFuel = 1f;
        HUDController.JetpackFuelDisplay.fillAmount = JetpackFuel;
        coroutine = null;
    }

    private Vector2 GenerateRandomForce()
    {
        return new Vector2(UnityEngine.Random.Range(-ForceModifier, ForceModifier), UnityEngine.Random.Range(-ForceModifier, ForceModifier));
    }
}