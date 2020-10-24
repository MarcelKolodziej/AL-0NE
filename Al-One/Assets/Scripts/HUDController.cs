﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("References To Assign")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject CrystalHUD;
    [SerializeField] private GameObject JetPackFlueDisplayParent;
    [SerializeField] private Animator SceenTranstionAnimator;
    public Image JetpackFuelDisplay;

    [Header("Settings")]
    [SerializeField] private bool DisplayMainMenuOnEnable = false;
    [SerializeField] private bool ShowJetPackDisplay = false;
    [SerializeField] private bool TurnOnCrystalHUD = false;


    private void OnEnable()
    {
        MainMenu.SetActive(DisplayMainMenuOnEnable && GameManager.Instance.MainMenuClosed == false);
        CrystalHUD.SetActive(TurnOnCrystalHUD);
        JetPackFlueDisplayParent.SetActive(ShowJetPackDisplay);
    }

    public void OnOpenMainMenuPressed()
    {
        MainMenu.SetActive(true);
    }

    public void SetJetpackGUIOn()
    {
        JetPackFlueDisplayParent.SetActive(true);
    }

    public void TransitionToScene(SceneData.Scene scene)
    {
        StartCoroutine(WaitForTransitionToScene(scene));
    }

    public void TransitionToScene(int sceneIndex)
    {
        StartCoroutine(WaitForTransitionToScene((SceneData.Scene) sceneIndex));
    }

    private IEnumerator WaitForTransitionToScene(SceneData.Scene scene)
    {
        SceenTranstionAnimator.SetBool("TriggerExit", true);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.LoadLevel(SceneData.GetSceneStringFromEnum(scene));
    }
}
