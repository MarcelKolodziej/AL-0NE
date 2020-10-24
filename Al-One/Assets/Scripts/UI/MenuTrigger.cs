using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private bool IsMainMenu = false;
    private bool IsNearMenu = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsNearMenu = true;

        if (IsMainMenu)
        {
            if (GameManager.Instance.MainMenuClosed == false)
            {
                Menu.SetActive(true);
            }
        }
        else
        {
            Menu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsNearMenu = false;
        Menu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit") && IsNearMenu)
        {
            Menu.SetActive(true);
        }
    }
}
