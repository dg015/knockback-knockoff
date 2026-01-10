using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedChecker : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Image speedBar;
    public bool KillSpeed;
    [SerializeField] private GameObject fireball;
    [SerializeField] private SpriteRenderer fireballSpriteRenderer;
    [SerializeField] private float fireballTransparency;
    [SerializeField] private float maxTransperency;

    [SerializeField] private float startFireBallSpeed;
    [SerializeField] private float maxspeed;
    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        speedBar = GameObject.Find("bar fill").GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {

        fill();
        killSpeed();
        startFireball();
        fireballIntensitiy();
    }

    private void killSpeed()
    {
        if(playerController.PVelocity.magnitude / maxspeed >= 1)
        {
            KillSpeed = true;
            Debug.Log("kill speed");
           
        }
        else
        {
            KillSpeed = false;
        }


    }

    private void startFireball()
    {
        
        if(playerController.PVelocity.magnitude/ maxspeed >= startFireBallSpeed/maxspeed)
        {
            Debug.Log("started");
            fireball.SetActive(true);
            
        }
        else
        {
            fireball.SetActive(false);
        }

    }

    //transparancy max 150
    //transparancy min 50
    private void fireballIntensitiy()
    {
        
        //normalize both 
        fireballTransparency = (playerController.PVelocity.magnitude / maxspeed) * (maxTransperency / 255);
        //fireballTransparency = Mathf.Clamp(fireballTransparency, 50 / 255, maxTransperency / 255);
        fireballSpriteRenderer.color = new Color(1f, 1f, 1f, fireballTransparency);
       
    }


    private void fill()
    {
        
        speedBar.fillAmount = Mathf.Clamp01(playerController.PVelocity.magnitude / maxspeed);

    }

}
