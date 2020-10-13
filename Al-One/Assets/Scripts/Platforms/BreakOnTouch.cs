using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnTouch : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float breakDelay = 1f;

    /// <summary>
    /// Detect Collion with player character and start break block timer
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("WaitForBreak");
        }
    }

    /// <summary>
    /// Wait then break
    /// </summary>
    private IEnumerator WaitForBreak()
    {
        yield return new WaitForSeconds(breakDelay);
        gameObject.SetActive(false);
    }
}
