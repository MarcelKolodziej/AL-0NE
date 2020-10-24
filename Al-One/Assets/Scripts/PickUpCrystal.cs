using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCrystal : MonoBehaviour
{
    [SerializeField] private CrystalDisplay CrystalDisplay;
    [SerializeField] private CrystalType CrystalOnDisplay;

    private enum CrystalType
    {
        GreenCrystal,
        BlueCrystal,
        RedCrystal,
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (CrystalOnDisplay)
        {
            case CrystalType.GreenCrystal:
                GameManager.Instance.GreenCrystalPickedUp = true;
                break;
            case CrystalType.BlueCrystal:
                GameManager.Instance.BlueCrystalPickedUp = true;
                break;
            case CrystalType.RedCrystal:
                GameManager.Instance.RedCrystalPickedUp = true;
                break;
        }

        GameManager.Instance.GetSoundPlayer().PlayCrystalPickedUpSFX();
        CrystalDisplay.UpdateCrystalDisplay();
        gameObject.SetActive(false);
    }
}
