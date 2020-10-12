using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Objects to assign")]
    [SerializeField] private Transform MovingTile;
    [SerializeField] private Transform[] Locations;

    [Header("Settings")]
    [SerializeField] private AnimationCurve AnimationCurve = AnimationCurve.Linear(0,0,1,1);
    [SerializeField] private int StartingLocation = 0;
    [SerializeField] private float TileMoveSpeed = 1f;

    [Header("Public Fields")]
    public bool IsActive = true;
    public bool PlayOneshot = false; // Added this so we can sequence a bunch of platforms at the same time  and ensure they stay in sync

    private float startTime;
    private float step;
    private int currentLocationIndex = 0;
    private int targetLocationIndex = 0;
    private float journeyLenght;

    private void Start()
    {
        MovingTile.position = Locations[StartingLocation].position;
        currentLocationIndex = StartingLocation -1;  // this is stupid, but it prevents duplicate code
        targetLocationIndex = StartingLocation;
        UpdatePlatformTargets();
    }

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

                if (PlayOneshot)
                {
                    PlayOneshot = false;
                    IsActive = false;
                }
            }
        }
    }
}
