using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        CloseMenu();
    }

    public void OnCreditsButtonPressed()
    {
        // Show Credits Here
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        GameManager.Instance.MainMenuClosed = false;
    }

    private void CloseMenu()
    {
        GameManager.Instance.MainMenuClosed = true;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OpenMenu();
    }

    private void OnDisable()
    {
        CloseMenu();
    }
}
