using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [Header("References To Assign")]
    [SerializeField] private GameObject CrystalHUD;
    [SerializeField] private Animator SceenTranstionAnimator;

    [Header("Settings")]
    [SerializeField] private bool TurnOnCrystalHUD = false;

    private void OnEnable()
    {
        CrystalHUD.SetActive(TurnOnCrystalHUD);
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
