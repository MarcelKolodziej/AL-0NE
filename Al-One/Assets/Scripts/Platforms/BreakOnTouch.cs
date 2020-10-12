using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnTouch : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float breakDelay = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("WaitForBreak");
        }
    }
    private IEnumerator WaitForBreak()
    {
        yield return new WaitForSeconds(breakDelay);
        gameObject.SetActive(false);
    }
}
