using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuObject;
    public GameObject CreditsObject;
    public GameObject SettingsObject;
    [Space]
    public AudioMixer AudioMixer;

    public void OnStartButtonPressed()
    {
        CloseMenu();
    }

    public void OnCreditsButtonPressed()
    {
        MainMenuObject.SetActive(false);
        CreditsObject.SetActive(true);
        SettingsObject.SetActive(false);
    }

    public void OnCloseCreditsButtonPressed()
    {
        MainMenuObject.SetActive(true);
        CreditsObject.SetActive(false);
        SettingsObject.SetActive(false);
    }

    public void OnOpenSettingsButtonPressed()
    {
        MainMenuObject.SetActive(false);
        CreditsObject.SetActive(false);
        SettingsObject.SetActive(true);
    }

    public void OnCloseSettingsButtonPressed()
    {
        MainMenuObject.SetActive(true);
        CreditsObject.SetActive(false);
        SettingsObject.SetActive(false);
    }

    public void OnMusicSliderUpdated(float Value)
    {
        AudioMixer.SetFloat("MusicVolume", Value);
    }

    public void OnSFXSliderUpdated(float Value)
    {
        AudioMixer.SetFloat("SFXVolume", Value);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        MainMenuObject.SetActive(true);
        CreditsObject.SetActive(false);
        SettingsObject.SetActive(false);
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
