using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRisingWater : MonoBehaviour
{
    [Header("Objects To Assign")]
    [SerializeField] private RisingWater RisingWater;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            RisingWater.ResetRisingWater();
        }
    }
}
