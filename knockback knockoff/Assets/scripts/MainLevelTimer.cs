using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MainLevelTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerText;
    public static float elaspedTime;
    public bool stopTimer;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (!stopTimer)
            increaseTime();
        else
            TimerText.color = Color.green;
        
        int minutes = Mathf.FloorToInt(elaspedTime / 60);
        int seconds = Mathf.FloorToInt(elaspedTime % 60);
        
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void increaseTime()
    {
        elaspedTime += Time.deltaTime;

    }

    public void setText(GameObject textObj)
    {
        textObj.GetComponent<TextMeshProUGUI>().text = elaspedTime.ToString();
        //TimerText.text = elaspedTime.ToString();
    }



}
