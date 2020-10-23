using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject CreditsObject;

    public void OnStartButtonPressed()
    {
        CloseMenu();
    }

    public void OnCreditsButtonPressed()
    {
        CreditsObject.SetActive(true);
    }

    public void OnCloseCreditsButtonPressed()
    {
        CreditsObject.SetActive(false);
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
        CreditsObject.SetActive(false);
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
