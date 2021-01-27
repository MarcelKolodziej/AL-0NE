using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InteractableButton : MonoBehaviour
{
    // we can swap this out for an interface later so that it will work with multiple scripts, not just the PlatformMover
    [SerializeField] private PlayerControls PlayerControls;
    [SerializeField] private PlatformMover Door;
    [SerializeField] private Animator ButtonAnimator;
    [SerializeField] private SoundPlayer SoundPlayer;
    [SerializeField] private CinemachineStateDrivenCamera MainCamera;

    private IEnumerator coroutine;
    private GameObject playerObject;

    private bool pressed = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!pressed && collider.gameObject.tag == "Player")
        {
            if (PlayerControls.PlayerHasControl)
            {
                playerObject = collider.gameObject;
                coroutine = DoorOpenSequence();
                StartCoroutine(coroutine);
            }
        }
    }

    private IEnumerator DoorOpenSequence()
    {
        PlayerControls.PlayerHasControl = false;
        PlayerControls.ResetAnimator();
        ButtonAnimator.SetBool("SwitchFlipped", true);
        SoundPlayer.PlayRockFallSFX();
        yield return new WaitForSeconds(0.7f);

        MainCamera.Follow = Door.gameObject.transform;
        yield return new WaitForSeconds(1f);

        Door.PlayOneShot();
        SoundPlayer.PlayHeavyDoorOpenSFX();
        pressed = true;
        yield return new WaitForSeconds(2f);
        MainCamera.Follow = playerObject.transform;
        yield return new WaitForSeconds(0.2f);
        PlayerControls.PlayerHasControl = true;
    }
}
