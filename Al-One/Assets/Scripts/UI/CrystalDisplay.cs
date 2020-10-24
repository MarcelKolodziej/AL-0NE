using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDisplay : MonoBehaviour
{
    [SerializeField] private GameObject GreenCrystal;
    [SerializeField] private GameObject BlueCrystal;
    [SerializeField] private GameObject RedCrystal;

    public void OnEnable()
    {
        UpdateCrystalDisplay();
    }

    public virtual void UpdateCrystalDisplay()
    {
        BlueCrystal.SetActive(GameManager.Instance.BlueCrystalPickedUp);
        GreenCrystal.SetActive(GameManager.Instance.GreenCrystalPickedUp);
        RedCrystal.SetActive(GameManager.Instance.RedCrystalPickedUp);
    }
}
