using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    // we can swap this out for an interface later so that it will work with multiple scripts, not just the PlatformMover
    [SerializeField] private PlatformMover Door;
    [SerializeField] private Animator ButtonAnimator;
    private bool pressed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!pressed && collision.gameObject.tag == "Player")
        {
            Door.PlayOneShot();
            ButtonAnimator.SetBool("SwitchFlipped", true);
            pressed = true;
        }
    }
}
