using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeWorldSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject FallStopper;
    [SerializeField] private GameObject NewFallStopperLocation;
    private bool SpawnPointSet = false;

    /// <summary>
    /// Detect Collion with player character and start break block timer
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !SpawnPointSet)
        {
            SpawnPointSet = true;
            GameManager.Instance.SetSpawnPoint(gameObject);
            FallStopper.transform.position = NewFallStopperLocation.transform.position;
        }
    }
}
