using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

/// <summary>
/// Game Manager Singleton, use this for keeping track of player progress, loading levels, health, respawning the player
/// </summary>
public class GameManager : Singleton<GameManager>
{
    private PlayerControls playerControls;
    private Transform spawnPoint;
    public bool MainMenuClosed = false;

    // for crystals collected
    public bool BlueCrystalPickedUp = false;
    public bool RedCrystalPickedUp = false;
    public bool GreenCrystalPickedUp = false;

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
        GameObject gameObject;

        // Populate references
        SetSpawnPoint(GameObject.FindGameObjectWithTag("Respawn"));

        gameObject = GameObject.FindGameObjectWithTag("Player");
        playerControls = gameObject.GetComponent<PlayerControls>();

        gameObject = GameObject.FindGameObjectWithTag("CMVirtualCam");

        // Setup Game Scene
        RespawnPlayer();
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    public void SetSpawnPoint(GameObject SpawnPoint)
    {
        spawnPoint = SpawnPoint.transform;
    }
    public void RespawnPlayer()
    {
        playerControls.transform.position = spawnPoint.transform.position;
    }
    public void DamangePlayer() 
    {
        TriggerPlayerDeath();
    }

    public void TriggerPlayerDeath()
    {
        RespawnPlayer();
    }
}
