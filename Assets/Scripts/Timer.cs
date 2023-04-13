using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLeft = 180.0f;
    public TextMeshProUGUI countDownText;
    public bool timeIsOver;
    //private SpawnManager spawnManager;
    private void Start() 
    {
        //spawnManager = GameObject.FindObjectOfType<SpawnManager>();
    }
    // Update is called once per frame
    void Update()
    {
        CountDown();
    }
    void CountDown()
    {
        timeLeft -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeLeft/60);
        int seconds = Mathf.FloorToInt(timeLeft%60);

        if (minutes <= 0 && seconds <= 0)
        {
            timeIsOver = true;
            countDownText.text = "00:00"; 
                
        }
        else 
        {
            countDownText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
        }
    }
}
