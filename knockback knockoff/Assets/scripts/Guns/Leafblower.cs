using Cinemachine;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Leafblower : Gun
{
    [SerializeField] public float overheatMaximum;
    [SerializeField] public float heat;
    [SerializeField] public float minimumHeat;

    [SerializeField] private float heatTapTime;
    [SerializeField] private StudioEventEmitter shootingSound;
    private bool isPlayingSound = false;

    private void OnEnable()
    {
        //inputActions = new PlayerInputActions();
        //inputActions.Player.Enable();


        //inputActions.Player.Shoot.started += callShootMethod;
        //inputActions.Player.Shoot.performed += callShootMethod;
        //inputActions.Player.Shoot.canceled += callShootMethod;

        //inputActions.Player.Aim.started += readMousePositionInput;
        //inputActions.Player.Aim.performed += readMousePositionInput;
        //inputActions.Player.Aim.canceled += readMousePositionInput;


    }


    private void OnDisable()
    {

        //inputActions.Player.Shoot.started -= callShootMethod;
        //inputActions.Player.Shoot.performed -= callShootMethod;
        //inputActions.Player.Shoot.canceled -= callShootMethod;

        //inputActions.Player.Aim.started -= readMousePositionInput;
        //inputActions.Player.Aim.performed -= readMousePositionInput;
        //inputActions.Player.Aim.canceled -= readMousePositionInput;

        //inputActions.Player.Disable();



    }

    // Start is called before the first frame update
    void Start()
    {
        //cinemachine
        Cinemachine = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        shake = Cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        //player
        pivot = gameObject.transform.GetComponentInParent<Transform>();
        playerRb = gameObject.transform.GetComponentInParent<Rigidbody2D>();
        controller = gameObject.transform.GetComponentInParent<PlayerController>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
        aim();
        heating();
        firing();
        cooldown();
        heat = Mathf.Clamp(heat, 0, overheatMaximum);
        delayTime += Time.deltaTime;
    }

    private void firing()
    {
        if (readyToFire && isSelected)
        {

            if (isShooting)
            {
                delayTime = 0;
                heat += Time.deltaTime;

                shoot();

                //only play once
                if(!isPlayingSound)
                {
                    shootingSound.Play();
                    isPlayingSound = true;
                }

            }
            else
            {
                //disable is not holding the button anymore
                if(isPlayingSound)
                {
                    shootingSound.Stop();
                    isPlayingSound = false;
                }
            }

        }
        else
        {
            shootingSound.Stop();
            isPlayingSound = false;
        }

    }


    // idea, add delay for firing
    private void heating()
    {
        
        if (heat >= overheatMaximum)
        {
            
            readyToFire = false;
           

        }
        if ( heat < minimumHeat)
        {
            readyToFire = true;
            
        }
        

    }




    private void cooldown()
    {

        if (!isShooting && delayTime >timeBetweenShots)
        {
            
            heat -= Time.deltaTime;
        }
        
    }
}
