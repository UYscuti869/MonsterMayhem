using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    private TextMeshProUGUI warning;
    private float timeChecker = 0;
    private Color textColor;
    public float fadeInTime = 1;
    public float stayTime = 1;
    public float fadeOutTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        warning = GetComponent<TextMeshProUGUI>();
        textColor = warning.color;
    }

    // Update is called once per frame
    void Update()
    {
        Blinking();
    }
    public void Blinking()
    {
        timeChecker += Time.deltaTime;
        if (timeChecker <= fadeInTime)
        {
            warning.color = new Color(textColor.r, textColor.g, textColor.b, timeChecker/fadeInTime);
        }
        else if (timeChecker <= fadeInTime + stayTime)
        {
            warning.color = new Color(textColor.r, textColor.g, textColor.b, 1);
        }
        else if (timeChecker <= fadeInTime + stayTime + fadeOutTime)
        {
            warning.color= new Color(textColor.r, textColor.g, textColor.b, 1 -(timeChecker -(fadeInTime + stayTime)/fadeOutTime));
        }
        else
        {
            timeChecker = 0;
        }
    }
}
