using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWrapper : MonoBehaviour
{
    public float TeleportX;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 CurrentPlayerPosition = GameManager.Instance.GetPlayerPosition();
        Vector3 NewPlayerPosition = new Vector3(TeleportX, CurrentPlayerPosition.y, CurrentPlayerPosition.z);
        GameManager.Instance.SetPlayerPosition(NewPlayerPosition);
    }
}
