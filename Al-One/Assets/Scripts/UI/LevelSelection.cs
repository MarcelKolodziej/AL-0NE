using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private HUDController HUDController;
    [SerializeField] private Button ReturnHomeButton;

    public void OnLevelSelectionPressed(int sceneIndex)
    {
        HUDController.TransitionToScene(sceneIndex);
        gameObject.SetActive(false);
    }

    public void OpenLevelSelection()
    {
        gameObject.SetActive(true);
    }

    public void CloseLevelSelection()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ReturnHomeButton.interactable = (GameManager.Instance.BlueCrystalPickedUp && GameManager.Instance.GreenCrystalPickedUp && GameManager.Instance.RedCrystalPickedUp);
        OpenLevelSelection();
    }

    private void OnDisable()
    {
        CloseLevelSelection();
    }
}
