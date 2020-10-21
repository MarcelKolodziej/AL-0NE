using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private HUDController HUDController;


    public void OnLevelSelectionPressed(int sceneIndex)
    {
        HUDController.TransitionToScene(sceneIndex);
        gameObject.SetActive(false);
    }

    public void OpenLevelSelection()
    {
        gameObject.SetActive(true);
        GameManager.Instance.MainMenuClosed = false;
    }

    public void CloseLevelSelection()
    {
        GameManager.Instance.MainMenuClosed = true;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OpenLevelSelection();
    }

    private void OnDisable()
    {
        CloseLevelSelection();
    }
}
