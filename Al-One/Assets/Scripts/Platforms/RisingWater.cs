﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour
{
    [Header("Objects To Assign")]
    [SerializeField] private Transform StartingPoint;
    [SerializeField] private Transform EndingPoint;

    [Header("Public Fields")]
    [SerializeField] private float RisingSpeed = 1f;
    public bool IsActive = false;
    private float journeyLenght;

    private float startTime;

    public void ActivateRisingWater()
    {
        IsActive = true;
        startTime = Time.time;
        journeyLenght = Vector3.Distance(StartingPoint.position, EndingPoint.position);
    }

    public void ResetRisingWater()
    {
        transform.position = StartingPoint.position;
        IsActive = false;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.DamangePlayer();
            ResetRisingWater();
        }
    }
}
