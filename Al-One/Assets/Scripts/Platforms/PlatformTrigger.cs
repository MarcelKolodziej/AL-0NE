using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [Header("Objects To Assign")]
    [SerializeField] private PlatformMover Platform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Platform.PlayOneShot();
    }
}
