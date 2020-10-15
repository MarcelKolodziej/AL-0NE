using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnTouch : MonoBehaviour
{
    [Header("Objects To Assign")]
    [SerializeField] private Rigidbody2D ObjectRigidbody;

    [Header("Settings")]
    [SerializeField] private float DropDelay = 1f;
    [SerializeField] private float BreakDelay = 1f;

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
        yield return new WaitForSeconds(DropDelay);
        ObjectRigidbody.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(BreakDelay);
        gameObject.SetActive(false);
    }
}
