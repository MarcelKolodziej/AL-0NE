using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFXOnCollision : MonoBehaviour
{
    private enum SFX 
    {
        Spike_Death,
        Lava_Death,
        Falling_Death,
    }

    [SerializeField] private SFX SFXToPlay;

    public void PlaySFX()
    {
        switch (SFXToPlay)
        {
            case SFX.Spike_Death:
                GameManager.Instance.GetSoundPlayer().PlaySpikeDeathSFX();
                break;
            case SFX.Lava_Death:
                GameManager.Instance.GetSoundPlayer().PlayLavaDeathSFX();
                break;
            case SFX.Falling_Death:
                GameManager.Instance.GetSoundPlayer().PlayFallingDeathSFX();
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlaySFX();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlaySFX();
        }
    }
}
