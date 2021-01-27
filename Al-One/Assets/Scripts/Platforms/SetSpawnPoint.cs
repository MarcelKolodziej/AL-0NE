using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnPoint : MonoBehaviour
{
    /// <summary>
    /// Detect Collion with player character and start break block timer
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerControls>().PlayerHasControl)
            {
                GameManager.Instance.SetSpawnPoint(gameObject);
            }
        }
    }
}
