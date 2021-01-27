using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRisingWater : MonoBehaviour
{
    [Header("Objects To Assign")]
    [SerializeField] private RisingWater RisingWater;
    [SerializeField] private PlatformMover Door;
    private bool firstOpen = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && firstOpen == true)
        {
            Reset();
        }
    }

    public void TriggerResetAfterDeath()
    {
        if (RisingWater.IsActive)
        {
            Reset();
        }
    }

    private void Reset()
    {
        firstOpen = false;
        GameManager.Instance.GetSoundPlayer().PlayHeavyDoorOpenSFX(0.2f);
        RisingWater.ResetRisingWater();
        Door.PlayOneShot();
    }
}
