using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFXOnCollision : MonoBehaviour
{
    private enum SFX 
    {
        Bounce,
        Spike_Death,
        Lava_Death,
        Falling_Death,
    }

    [SerializeField] private SFX SFXToPlay;
    private SoundPlayer soundPlayer;

    public void PlaySFX()
    {
        if (soundPlayer == null)
        {
            soundPlayer = GameManager.Instance.GetSoundPlayer();
        }

        switch (SFXToPlay)
        {
            case SFX.Bounce:
                soundPlayer.PlayBounceSFX();
                break;
            case SFX.Spike_Death:
                soundPlayer.PlaySpikeDeathSFX();
                break;
            case SFX.Lava_Death:
                soundPlayer.PlayLavaDeathSFX();
                break;
            case SFX.Falling_Death:
                soundPlayer.PlayFallingDeathSFX();
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
