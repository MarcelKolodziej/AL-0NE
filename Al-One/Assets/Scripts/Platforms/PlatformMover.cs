using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Objects to assign")]
    [SerializeField] private Transform MovingTile;
    [SerializeField] private Transform[] Locations;

    [Header("Settings")]
    [SerializeField] private byte LocationIndex = 0;
    [SerializeField] private float TileSpeed =1f;

    private float step;

    private void Update()
    {
        if (MovingTile.position != Locations[LocationIndex].position)
        {
            step = TileSpeed * Time.deltaTime;
            MovingTile.position = Vector3.MoveTowards(MovingTile.position, Locations[LocationIndex].position, step);
        }
        else
        {
            LocationIndex++;

            if (LocationIndex > Locations.Length-1)
            {
                LocationIndex = 0;
            }
        }
    }
}
