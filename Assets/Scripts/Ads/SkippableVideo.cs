using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkippableVideo : MonoBehaviour
{
    private AdsController _AdsController;

    void Start()
    {
        _AdsController = FindObjectOfType(typeof(AdsController)) as AdsController;

        StartCoroutine(StartSkippableVideo());
    }

    IEnumerator StartSkippableVideo()
    {
        yield return new WaitForSeconds(5);
        _AdsController.LoadSkippableVideo();
    }
}
