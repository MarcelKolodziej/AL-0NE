using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

/// <summary>
/// Game Manager Singleton, use this for keeping track of player progress, loading levels, health, respawning the player
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager gameManager = null;

    public static GameManager Instance
    {
        get
        {
            return gameManager;
        }
    }

    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private GameObject CameraPrefab;

    private PlayerControls playerControls;
    private Transform spawnPoint;
    private CinemachineVirtualCamera cmvCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        gameManager = this;
        DontDestroyOnLoad(gameObject);
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

        gameObject = GameObject.Instantiate(PlayerPrefab);
        playerControls = gameObject.GetComponent<PlayerControls>();

        gameObject = GameObject.Instantiate(CameraPrefab);
        cmvCamera = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        cmvCamera.Follow = playerControls.transform;

        // Setup Game Scene
        RespawnPlayer();
    }

    public void SetSpawnPoint(GameObject SpawnPoint)
    {
        spawnPoint = SpawnPoint.transform;
    }
    public void RespawnPlayer()
    {
        playerControls.transform.position = spawnPoint.transform.position;
        cmvCamera.transform.position = spawnPoint.transform.position;
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
