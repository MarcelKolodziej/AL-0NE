using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSequencerTrigger : MonoBehaviour
{
    [Header("Objects To Assign")]
    [SerializeField] private PlatformSequencer PlatformSequencer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlatformSequencer.IsActive = true;
        PlatformSequencer.StartSequence();
    }
}
