using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public void OnWaterWorldPressed()
    {
        /// GameManager.Instance.LoadLevel("");
        Debug.LogError("Please Assign Level");
    }

    public void OnLavaWorldPressed()
    {
        GameManager.Instance.LoadLevel("LavaPlanet");
    }

    public void OnTreeWorldPressed()
    {
        GameManager.Instance.LoadLevel("Tree Planet");
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
