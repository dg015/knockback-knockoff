using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedChecker : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Image speedBar;
    [SerializeField] private bool maxSpeedReached;
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
            maxSpeedReached = true;

        }
        else
        {
            maxSpeedReached = false;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.collider.CompareTag("Player"))
        {
            if (maxSpeedReached)
            {
                Destroy(collision.gameObject);
                Debug.Log("kill");
            }
        }
    }

    

    private void fill()
    {
        speedBar.fillAmount = rb.linearVelocity.magnitude / maxspeed;
    }

}
