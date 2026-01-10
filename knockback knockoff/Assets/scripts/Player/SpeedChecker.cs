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

    

    private void fill()
    {
        
        speedBar.fillAmount = Mathf.Clamp01(playerController.PVelocity.magnitude / maxspeed);
    }

}
