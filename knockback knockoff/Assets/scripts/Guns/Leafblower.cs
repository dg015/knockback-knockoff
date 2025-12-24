using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Leafblower : Gun
{
    [SerializeField] private float overheatMaximum;
    [SerializeField] private float heat;
    [SerializeField] private float minimumHeat;

    [SerializeField] private float StartUpHeat;



    private void OnEnable()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();


        inputActions.Player.Shoot.started += callShootMethod;
        inputActions.Player.Shoot.performed += callShootMethod;
        inputActions.Player.Shoot.canceled += callShootMethod;

    }


    private void OnDisable()
    {

        inputActions.Player.Shoot.started -= callShootMethod;
        inputActions.Player.Shoot.performed -= callShootMethod;
        inputActions.Player.Shoot.canceled -= callShootMethod;
        inputActions.Player.Disable();


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
        if (readyToFire)
        {

            if (isShooting)
            {
                delayTime = 0;
                heat += Time.deltaTime;
                shoot();
            }

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
        if (!Input.GetMouseButton(0) && delayTime >timeBetweenShots)
        {
            
            heat -= Time.deltaTime;
        }
        
    }
}
