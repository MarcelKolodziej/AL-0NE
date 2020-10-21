using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorDisplay : CrystalDisplay
{
    [SerializeField] private GameObject ReactorTop;

    public override void UpdateCrystalDisplay()
    {
        base.UpdateCrystalDisplay();
        ReactorTop.SetActive(GameManager.Instance.BlueCrystalPickedUp && GameManager.Instance.GreenCrystalPickedUp && GameManager.Instance.RedCrystalPickedUp);
    }
}
