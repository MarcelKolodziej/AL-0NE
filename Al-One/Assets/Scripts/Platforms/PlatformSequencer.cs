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

    private Coroutine coroutine;

    private void Start()
    {
        coroutine = StartCoroutine("MoveSequence");
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
            coroutine = StartCoroutine("MoveSequence");
        }
    }

    /// <summary>
    /// Forces stops the coroutine and all referenced platforms.
    /// </summary>
    public void ForceStop()
    {
        StopCoroutine(coroutine);
        coroutine = null;

        foreach (PlatformMover platform in PlatformMovers)
        {
            platform.IsActive = false;
        }
    }

    /// <summary>
    /// Coroutine for the actual sequence
    /// </summary>
    private IEnumerable MoveSequence()
    {
        while (IsActive)
        {
            yield return new WaitForSeconds(StartDelay);

            foreach (PlatformMover platform in PlatformMovers)
            {
                platform.PlayOneshot = true;
                yield return new WaitForSeconds(TickDelay);
            }

            yield return new WaitForSeconds(OpenDelay);

            foreach (PlatformMover platform in PlatformMovers)
            {
                platform.PlayOneshot = true;
                yield return new WaitForSeconds(TickDelay);
            }

            yield return new WaitForSeconds(CloseDelay);
        }
    }
}
