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
    private SoundPlayer soundPlayer;
    private Transform spawnPoint;
    public bool MainMenuClosed = false;
    public SceneData.Scene CurrentLevelName;

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
        CurrentLevelName = SceneData.GetEnumFromSceneString(scene.name);
        GameObject gameObject;

        // Populate references
        SetSpawnPoint(GameObject.FindGameObjectWithTag("Respawn"));

        gameObject = GameObject.FindGameObjectWithTag("Player");
        playerControls = gameObject.GetComponent<PlayerControls>();
        soundPlayer = gameObject.GetComponentInChildren<SoundPlayer>();

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

    public void SetPlayerPosition(Vector3 position)
    {
        playerControls.transform.position = position;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerControls.transform.position;
    }

    public SoundPlayer GetSoundPlayer()
    {
        return soundPlayer;
    }
}
