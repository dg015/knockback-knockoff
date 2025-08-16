using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainLevelTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerText;
    float elaspedTime;
    public bool stopTimer;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (!stopTimer)
            increaseTime();
        else
            TimerText.color = Color.green;
        setText();
        int minutes = Mathf.FloorToInt(elaspedTime / 60);
        int seconds = Mathf.FloorToInt(elaspedTime % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void increaseTime()
    {
        elaspedTime += Time.deltaTime;

    }

    private void setText()
    {
        TimerText.text = elaspedTime.ToString();
    }

}
