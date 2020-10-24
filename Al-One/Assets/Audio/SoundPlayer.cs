using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundPlayer : MonoBehaviour
{
    [Header("Themes")]
    [SerializeField] private AudioSource HubPlanetTheme;
    [SerializeField] private AudioSource LavaPlanetTheme;
    [SerializeField] private AudioSource TreePlanetTheme;
    [SerializeField] private AudioSource WaterPlanetTheme;
    [SerializeField] private AudioSource HomePlanetTheme;
    [Header("SFX")]
    [SerializeField] private AudioSource CrystalCollectedSFX;

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
                HubPlanetTheme.Play();
                break;
            case SceneData.Scene.LavaPlanet:
                LavaPlanetTheme.Play();
                break;
            case SceneData.Scene.TreePlanet:
                TreePlanetTheme.Play();
                break;
            case SceneData.Scene.WaterPlanet:
                WaterPlanetTheme.Play();
                break;
            case SceneData.Scene.HomePlanet:
                HomePlanetTheme.Play();
                break;
        }

    }
}
