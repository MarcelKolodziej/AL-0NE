using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Objects To Assign")]
    [SerializeField] private Transform MovingTile;
    [SerializeField] private Transform[] Locations;

    [Header("Settings")]
    [SerializeField] private AnimationCurve AnimationCurve = AnimationCurve.Linear(0, 0, 1, 1); // used to make the platform movements look more animated
    [SerializeField] private int StartingLocation = 0;
    [SerializeField] private float TileMoveSpeed = 1f;

    [Header("Public Fields")]
    public bool IsActive = true;
    private bool playOneshot = false;
    // Added this so we can sequence a bunch of platforms together at the same time  and ensure they stay in sync

    // maybe re-work this to use time instead of speed, might make it easier to manage
    private float startTime;
    private int currentLocationIndex = 0;
    private int targetLocationIndex = 0;
    private float journeyLenght;

    private void Start()
    {
        ResetToDefaultPosition();
    }

    /// <summary>
    /// Used to determin current location and destination
    /// </summary>
    private void UpdatePlatformTargets()
    {
        currentLocationIndex++;

        if (currentLocationIndex > Locations.Length - 1)
        {
            currentLocationIndex = 0;
        }

        targetLocationIndex++;

        if (targetLocationIndex > Locations.Length - 1)
        {
            targetLocationIndex = 0;
        }

        startTime = Time.time;
        journeyLenght = Vector3.Distance(Locations[currentLocationIndex].position, Locations[targetLocationIndex].position);
    }

    /// <summary>
    /// Forces the platform to move in and out once.
    /// </summary>
    public void PlayOneShot()
    {
        if (!IsActive)
        {
            playOneshot = true;
            IsActive = true;
            startTime = Time.time;
            journeyLenght = Vector3.Distance(Locations[currentLocationIndex].position, Locations[targetLocationIndex].position);
        }
    }

    private void Update()
    {
        if (IsActive)
        {
            float distanceCovered = (Time.time - startTime) * TileMoveSpeed;

            if (distanceCovered < journeyLenght)
            {
                float fractionOfJourney = distanceCovered / journeyLenght;

                MovingTile.position = Vector3.Lerp(Locations[currentLocationIndex].position, Locations[targetLocationIndex].position, AnimationCurve.Evaluate(fractionOfJourney));
            }
            else
            {
                UpdatePlatformTargets();

                if (playOneshot)
                {
                    playOneshot = false;
                    IsActive = false;
                }
            }
        }
    }

    /// <summary>
    /// Resets the platform to it's default position.
    /// </summary>
    public void ResetToDefaultPosition(bool turnOff = false)
    {
        if (turnOff)
        {
            IsActive = false;
        }

        MovingTile.position = Locations[StartingLocation].position;
        currentLocationIndex = StartingLocation - 1;  // this is stupid, but it prevents duplicate code
        targetLocationIndex = StartingLocation;
        UpdatePlatformTargets();
    }
}
