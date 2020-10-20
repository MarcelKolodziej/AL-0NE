using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemipermeablePlatform : MonoBehaviour
{
    [SerializeField] private PlatformEffector2D PlatformEffector2D;
    [SerializeField] private float WaitTime;
    private float timeToWait;
    private bool isTouchingPlayer;

    private void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");

        if (moveVertical < 0f)
        {
            if (timeToWait <= 0)
            {
                PlatformEffector2D.rotationalOffset = 180f;
                timeToWait = WaitTime;
            }
            else
            {
                timeToWait -= Time.deltaTime;
            }
        }

        if (Input.GetAxis("Jump") > 0f)
        {
            PlatformEffector2D.rotationalOffset = 0;
        }

        if ((moveVertical > 0f || Input.GetAxis("Jump") < 0f) && !isTouchingPlayer)
        {
            PlatformEffector2D.rotationalOffset = 0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isTouchingPlayer = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingPlayer = false;
    }
}
