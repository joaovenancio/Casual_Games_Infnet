using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    //private AdsController _AdsController;

    [SerializeField] TMP_Text txtTime;
    [SerializeField] float timeValue;

    // Start is called before the first frame update
    void Start()
    {
        //_AdsController = FindObjectOfType(typeof(AdsController)) as AdsController;
        InvokeRepeating("DecreaseTime", 1f, 1f);
    }

    private void DecreaseTime()
    {
        if(timeValue < 0f) return;

        if(timeValue > 0f)
        {
            timeValue--;
        }

        else
        {
            timeValue = 0f;
            SceneManager.LoadScene("GameOver");            
        }

        DisplayTime(timeValue);
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0f)
        {
            timeToDisplay = 0f;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        txtTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
}
