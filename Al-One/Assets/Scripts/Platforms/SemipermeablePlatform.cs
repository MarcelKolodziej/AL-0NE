using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemipermeablePlatform : MonoBehaviour
{
    [SerializeField] private PlatformEffector2D PlatformEffector2D;
    [SerializeField] private float WaitTime;
    private float timeToWait;

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

        if (moveVertical > 0f)
        {
            PlatformEffector2D.rotationalOffset = 0f;
        }
    }
}
