using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedChecker : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Image speedBar;
    public bool KillSpeed;

    [SerializeField] private float maxspeed;
    // Start is called before the first frame update
    void Start()
    {
        rb= gameObject.GetComponent<Rigidbody2D>();
        speedBar = GameObject.Find("bar fill").GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {

        fill();
        killSpeed();
    }

    private void killSpeed()
    {
        if(rb.linearVelocity.magnitude / maxspeed > 1)
        {
            KillSpeed = true;
            Debug.Log("kill speed");

        }
        else
        {
            KillSpeed = false;
        }


    }

    

    private void fill()
    {
        speedBar.fillAmount = rb.linearVelocity.magnitude / maxspeed;
    }

}
