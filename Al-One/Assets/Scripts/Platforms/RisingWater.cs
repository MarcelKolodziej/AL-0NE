using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour
{
    [Header("Objects To Assign")]
    [SerializeField] private Transform StartingPoint;
    [SerializeField] private Transform EndingPoint;
    [Space]
    [SerializeField] private PlatformMover PlatformMover;
    [SerializeField] private PlatformSequencer PlatformSequencer;

    [Header("Public Fields")]
    [SerializeField] private float RisingSpeed = 1f;
    public bool IsActive = false;
    private float journeyLenght;

    private float startTime;

    public void ActivateRisingWater()
    {
        if (!IsActive)
        {
            IsActive = true;
            startTime = Time.time;
            journeyLenght = Vector3.Distance(StartingPoint.position, EndingPoint.position);
        }
    }

    public void ResetRisingWater()
    {
        IsActive = false;
        PlatformMover.ResetToDefaultPosition(true);
        PlatformSequencer.ForceStop(true);
        transform.position = StartingPoint.position;
    }
    
    private void Update()
    {
        if (IsActive)
        {
            float distanceCovered = (Time.time - startTime) * RisingSpeed;

            if (distanceCovered < journeyLenght)
            {
                float fractionOfJourney = distanceCovered / journeyLenght;

                transform.position = Vector3.Lerp(StartingPoint.position, EndingPoint.position, fractionOfJourney);
            }
            else
            {
                transform.position = EndingPoint.position;
                IsActive = false;
            }
        }
    }
}
