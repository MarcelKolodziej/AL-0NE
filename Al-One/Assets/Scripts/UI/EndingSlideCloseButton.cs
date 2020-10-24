using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSlideCloseButton : MonoBehaviour
{
    public void OnCloseButtonPressed()
    {
        gameObject.SetActive(false);
    }
}
