using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToHub : MonoBehaviour
{
    [SerializeField] private HUDController HUDController;
    [SerializeField] private SceneData.Scene SceneToOpen;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        HUDController.TransitionToScene(SceneToOpen);
    }
}
