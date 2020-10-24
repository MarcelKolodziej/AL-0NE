using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSlideCloseButton : MonoBehaviour
{
    [SerializeField] private GameObject EndingMessage;
    [SerializeField] private GameObject KonamiHintMessage;


    private void OnEnable()
    {
        EndingMessage.SetActive(true);
        KonamiHintMessage.SetActive(false);
    }
    public void OnCloseButtonPressed()
    {
        gameObject.SetActive(false);
    }

    public void OnNextButtonPressed()
    {
        EndingMessage.SetActive(false);
        KonamiHintMessage.SetActive(true);
    }
}
