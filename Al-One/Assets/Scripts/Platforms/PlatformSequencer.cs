using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSequencer : MonoBehaviour
{
    [Header("Objects To Assign")]
    [SerializeField] private PlatformMover[] PlatformMovers;

    [Header("Settings")]
    [SerializeField] private float StartDelay = 0f;
    [SerializeField] private float TickDelay = 0f;
    [SerializeField] private float OpenDelay = 0f;
    [SerializeField] private float CloseDelay = 0f;
    public bool IsActive = true;

    private IEnumerator coroutine;

    private void Start()
    {
        if (IsActive)
        {
            coroutine = MoveSequence();
            StartCoroutine(coroutine);
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine); // prevents memory loss
        coroutine = null;
    }

    /// <summary>
    /// Used to start the sequence from script instead of awake.
    /// </summary>
    public void StartSequence()
    {
        if (coroutine == null)
        {
            coroutine = MoveSequence();
            StartCoroutine(coroutine);
        }
    }

    /// <summary>
    /// Forces stops the coroutine and all referenced platforms.
    /// </summary>
    public void ForceStop(bool turnOff = false)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        } 
        coroutine = null;

        foreach (PlatformMover platform in PlatformMovers)
        {
            platform.IsActive = false;
            platform.ResetToDefaultPosition(turnOff);
        }
    }

    /// <summary>
    /// Coroutine for the actual sequence
    /// </summary>
    private IEnumerator MoveSequence()
    {
        yield return new WaitForSeconds(StartDelay);

        while (IsActive)
        {
            foreach (PlatformMover platform in PlatformMovers)
            {
                platform.PlayOneShot();
                yield return new WaitForSeconds(TickDelay);
            }

            yield return new WaitForSeconds(OpenDelay);

            foreach (PlatformMover platform in PlatformMovers)
            {
                platform.PlayOneShot();
                yield return new WaitForSeconds(TickDelay);
            }

            yield return new WaitForSeconds(CloseDelay);
        }
    }
}
