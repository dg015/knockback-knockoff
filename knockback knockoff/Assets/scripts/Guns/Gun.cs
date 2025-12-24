using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    public int gunID;
    //spawn
    [Header("Spawning bullets")]
    [SerializeField] protected Transform pivot;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected bool noBarrel;
    [SerializeField] protected Transform barrel;


    [Header("Knockback")]
    [SerializeField] protected Rigidbody2D playerRb;
    [SerializeField] public float force;

    protected Vector2 angle;
    protected float direction;

    [SerializeField] protected PlayerController controller;

    [Header("isShooting")]
    [SerializeField] protected float timeBetweenShots;
    protected float RunningTime;
     protected bool readyToFire;
     protected bool isShooting;

    [Header("Shake")]
    [SerializeField] protected CinemachineVirtualCamera Cinemachine;
    [SerializeField] protected CinemachineBasicMultiChannelPerlin shake;
    [SerializeField] protected float shakeIntensity;
    [SerializeField] protected float Shaketime;
    [SerializeField] protected float shaketimeMax;

    [Header("new Input system")]
    [SerializeField] protected PlayerInput playerInputComponent;
    [SerializeField] protected PlayerInputActions inputActions;
    


    // Start is called before the first frame update

    

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
    

    void Start()
    {
        readyToFire = true;
        playerInputComponent = GetComponentInParent<PlayerInput>();

        Cinemachine = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        shake = Cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        pivot = gameObject.transform.GetComponentInParent<Transform>();
       
        playerRb = gameObject.transform.GetComponentInParent<Rigidbody2D>();
        controller = gameObject.transform.GetComponentInParent<PlayerController>();
        if (!noBarrel)
            barrel = GameObject.Find("barrel").GetComponent<Transform>();
        //barrel = gameObject.transform.GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        RunningTime += Time.deltaTime;
        aim();
        CameraShakeTimer();
        
        if(readyToFire && isShooting)
        {
            StartCoroutine(isShootingCycle());
        }
        
    }

    protected void CameraShakeTimer()
    {
        // check if theres any shake on the camerea
        if (shake.m_AmplitudeGain > 0)
        {
            //increase shake time
            Shaketime += Time.deltaTime;
            if (Shaketime > shaketimeMax)
            {
                //set camera shake to 0 
                shake.m_AmplitudeGain = 0;
                Shaketime = 0;
            }

        }
    }
    



    public void callShootMethod(InputAction.CallbackContext context)
    {
        //Debug.Log("isShooting");
        
        string buttonControlPath = "/Mouse/leftButton";

        
        if (context.started)
        {
            if( context.control.path == buttonControlPath && readyToFire)
            {
                //Debug.Log("started action");
                isShooting = true;
            }
        }
       else if (context.performed )
        {
            if(context.control.path == buttonControlPath && readyToFire)
            {
               // Debug.Log("continuing action");
                isShooting = true;
            }
        }
        else if(context.canceled)
        {
            if(context.control.path == buttonControlPath)
            {
                //Debug.Log("Button released");
                isShooting = false;
            }
        }
    }

    protected IEnumerator isShootingCycle()
    {

        shoot();
        CameraShake();
        readyToFire = false;
        yield return new WaitForSeconds(timeBetweenShots);

        readyToFire = true;


    }

    protected void CameraShake()
    {
        //add camera shake 
        shake.m_AmplitudeGain = shakeIntensity;
    }

    public virtual void shoot()
    {

        //get players current speed
        Vector2 currentVelocity = playerRb.linearVelocity;
        
        //apply the direction of the mouse inversed as new position
        Vector2 KnockbackDirection = new Vector2();
        KnockbackDirection = -angle.normalized;

        //Apply force
        float initialVelocityInfluence = 0.3f;
        controller.PVelocity = KnockbackDirection * force + currentVelocity * initialVelocityInfluence;

        if(!noBarrel)
            Instantiate(bullet, barrel.position, barrel.rotation);

        CameraShakeTimer();
        CameraShake();
    }


    public void aim()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        angle = new Vector2 ((mousePos.x - pivot.position.x),(mousePos.y - pivot.position.y));
        direction = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,direction);


        /*
        if(transform.rotation.eulerAngles.x >= -90 || transform.rotation.eulerAngles.x <= 90)
        {
            transform.FindChild("Gun sprite").transform.localScale =  new Vector3(-1, 1, 1);
        }
        else
        {
            transform.FindChild("Gun sprite").transform.localScale =  = new Vector3(1, 1, 1);
        }
        */
    }
}
