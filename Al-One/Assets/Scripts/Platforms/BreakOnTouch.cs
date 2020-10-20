using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnTouch : MonoBehaviour
{
    [Header("Objects To Assign")]
    [SerializeField] private Rigidbody2D ObjectRigidbody;

    [Header("Settings")]
    [SerializeField] private float ResetDelay = 1f;
    [SerializeField] private bool DestroyAfterTouch = false;
    [SerializeField] private bool AutomaticReset = false;

    private Vector3 startPosition;
    private bool blockHasFallen = false;

    private void Start()
    {
        startPosition = gameObject.transform.position;
    }

    /// <summary>
    /// Used to reset the objects state to it's default
    /// </summary>
    public void ResetObjects()
    {
        gameObject.transform.position = startPosition;
        ObjectRigidbody.bodyType = RigidbodyType2D.Static;
        blockHasFallen = false;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Detect Collion with player character and start break block timer
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !blockHasFallen)
        {
            blockHasFallen = true;
            ObjectRigidbody.bodyType = RigidbodyType2D.Dynamic;
            ObjectRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            ObjectRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            if (AutomaticReset)
            {
                StartCoroutine("WaitForAutomaticReset");
            }
        }
        else if (!AutomaticReset)
        {
            if (collision.gameObject.tag != ("FallingBlock") && collision.gameObject.name != "Foreground" && collision.gameObject.tag != "Player")
            {
                if (DestroyAfterTouch)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    ObjectRigidbody.bodyType = RigidbodyType2D.Static;
                }
            }
        }
    }

    /// <summary>
    /// Wait then break
    /// </summary>
    private IEnumerator WaitForAutomaticReset()
    {
        yield return new WaitForSeconds(ResetDelay);

        if (DestroyAfterTouch)
        {
            gameObject.SetActive(false);
        }
        else
        {
            ResetObjects();
        }
    }
}
