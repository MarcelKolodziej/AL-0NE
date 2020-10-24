using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundPlayer : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource ThemeAudioSource;
    [SerializeField] private AudioSource SFXAudioSource;
    [Space]
    [SerializeField] private AudioSource JumpingAudioSource;
    [SerializeField] private AudioSource JetpackAudioSource;
    [SerializeField] private AudioSource CrystalCollectAudioSource;

[Header("Theme Clips")]
    [SerializeField] private AudioClip HubPlanetTheme;
    [SerializeField] private AudioClip LavaPlanetTheme;
    [SerializeField] private AudioClip TreePlanetTheme;
    [SerializeField] private AudioClip WaterPlanetTheme;
    [SerializeField] private AudioClip HomePlanetTheme;

    [Header("Death SFX Clips")]
    [SerializeField] private AudioClip SpikeDeath;
    [SerializeField] private AudioClip LavaDeath;
    [SerializeField] private AudioClip FallingDeath;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SceneData.Scene currentScene = SceneData.GetEnumFromSceneString(scene.name);

        switch (currentScene)
        {
            case SceneData.Scene.HubPlanet:
                ThemeAudioSource.clip = HubPlanetTheme;
                ThemeAudioSource.Play();
                break;
            case SceneData.Scene.LavaPlanet:
                ThemeAudioSource.clip = LavaPlanetTheme;
                ThemeAudioSource.Play();
                break;
            case SceneData.Scene.TreePlanet:
                ThemeAudioSource.clip = TreePlanetTheme;
                ThemeAudioSource.Play();
                break;
            case SceneData.Scene.WaterPlanet:
                ThemeAudioSource.clip = WaterPlanetTheme;
                ThemeAudioSource.Play();
                break;
            case SceneData.Scene.HomePlanet:
                ThemeAudioSource.clip = HomePlanetTheme;
                ThemeAudioSource.Play();
                break;
        }

    }

    public void PlayJetpackSound()
    {
        JetpackAudioSource.Play();
    }

    public void StopJetpackSound()
    {
        JetpackAudioSource.Stop();
    }

    public void PlayCrystalPickedUpSFX()
    {
        CrystalCollectAudioSource.Play();
    }

    public void PlayJumpingSFX()
    {
        JumpingAudioSource.Play();
    }

    public void PlaySpikeDeathSFX()
    {
        SFXAudioSource.volume = 0.8f;
        SFXAudioSource.clip = SpikeDeath;
        SFXAudioSource.Play();
    }

    public void PlayLavaDeathSFX()
    {
        if (!SFXAudioSource.isPlaying)
        {
            SFXAudioSource.volume = 1f;
            SFXAudioSource.clip = LavaDeath;
            SFXAudioSource.Play();
        }
    }

    public void PlayFallingDeathSFX()
    {
        if (!SFXAudioSource.isPlaying)
        {
            SFXAudioSource.volume = 1f;
            SFXAudioSource.clip = FallingDeath;
            SFXAudioSource.Play();
        }
    }
}
