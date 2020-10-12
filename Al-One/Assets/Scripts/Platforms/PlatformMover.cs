using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Transform MovingTile;
    public Transform[] Locations;

    public byte LocationIndex = 0;
    public float TileSpeed =1f;
    private float m_Step;
    private Vector3 m_Direction;

    private void Update()
    {
        if (MovingTile.position != Locations[LocationIndex].position)
        {
            m_Step = TileSpeed * Time.deltaTime;
            MovingTile.position = Vector3.MoveTowards(MovingTile.position, Locations[LocationIndex].position, m_Step);
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
