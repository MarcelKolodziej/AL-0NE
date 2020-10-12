using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSequencer : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine("Sequence");
    }

    private void OnDestroy()
    {
        StopCoroutine("Sequence");
    }

    private IEnumerable Sequence()
    {
        yield return null;
    }
}
