using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
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

    [Header("aiming")]
    protected Vector3 aimInput;
    protected Vector2 angle;
    protected float direction;

    [SerializeField] protected PlayerController controller;

    [Header("isShooting")]
    [SerializeField] protected float timeBetweenShots;
    [SerializeField] protected float delayTime;
    [SerializeField] protected bool readyToFire;
    protected bool isShooting;
    private string buttonControlPath;
    [SerializeField] protected float triggerInput;
    [SerializeField] private bool coroutineRunning;

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

        inputActions.Player.Aim.started += readMousePositionInput;
        inputActions.Player.Aim.performed += readMousePositionInput;
        inputActions.Player.Aim.canceled += readMousePositionInput;
    }


    private void OnDisable()
    {

        inputActions.Player.Shoot.started -= callShootMethod;
        inputActions.Player.Shoot.performed -= callShootMethod;
        inputActions.Player.Shoot.canceled -= callShootMethod;

        inputActions.Player.Aim.started -= readMousePositionInput;
        inputActions.Player.Aim.performed -= readMousePositionInput;
        inputActions.Player.Aim.canceled -= readMousePositionInput;

        inputActions.Player.Disable();


    }

    private void Awake()
    {
        playerInputComponent = GetComponentInParent<PlayerInput>();

        Cinemachine = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        shake = Cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        pivot = gameObject.transform.GetComponentInParent<Transform>();

        playerRb = gameObject.transform.GetComponentInParent<Rigidbody2D>();
        controller = gameObject.transform.GetComponentInParent<PlayerController>();
    }

    void Start()
    {
        readyToFire = true;

        if (!noBarrel)
            barrel = GameObject.Find("barrel").GetComponent<Transform>();
        //barrel = gameObject.transform.GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        delayTime += Time.deltaTime;
        aim();
        CameraShakeTimer();

        if (readyToFire && isShooting)
        {
            StartCoroutine(isShootingCycle());
            delayTime = 0;
            delayTime += Time.deltaTime;
            
        }
        else if (delayTime >= timeBetweenShots && readyToFire == false && !isShooting) // double check since coroutines stop working the moment they game object is disabled
        {
            Debug.Log("here");
            readyToFire = true;
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
       
        foreach (InputDevice device in playerInputComponent.devices)
        {
            if (device is Keyboard )
            {
                buttonControlPath = "/Mouse/leftButton";
                if (context.started)
                {
                    if (context.control.path == buttonControlPath && readyToFire)
                    {
                       //Debug.Log("started action");
                        isShooting = true;
                    }
                }
                else if (context.performed)
                {
                    if (context.control.path == buttonControlPath && readyToFire)
                    {
                         //Debug.Log("continuing action");
                        isShooting = true;
                    }
                }
                else if (context.canceled)
                {
                    if (context.control.path == buttonControlPath)
                    {
                        //Debug.Log("Button released");
                        isShooting = false;
                    }
                }
            }
            else if (device is Gamepad )
            {
                triggerInput = context.ReadValue<float>();
               

                if (context.started)
                {
                    if (triggerInput > 0.25f && readyToFire)
                    {
                        //Debug.Log("started action");
                        isShooting = true;
                    }
                }
                else if (context.performed)
                {
                    if (triggerInput > 0.25f && readyToFire)
                    {
                        // Debug.Log("continuing action");
                        isShooting = true;
                    }
                }
                else if (context.canceled)
                {
                    if (triggerInput < 0.25f)
                    {
                        //Debug.Log("Button released");
                        isShooting = false;
                    }
                }
            }
        }
    }

    protected IEnumerator isShootingCycle()
    {

        shoot();
        CameraShake();

        readyToFire = false;
        yield return new WaitForSeconds(timeBetweenShots);
        Debug.Log("also here"); 
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

        if (!noBarrel)
            Instantiate(bullet, barrel.position, barrel.rotation);

        CameraShakeTimer();
        CameraShake();

    }


    public void readMousePositionInput(InputAction.CallbackContext context)
    {
        
        aimInput = context.ReadValue<Vector2>();
        //Debug.Log(aimInput);
    }

    public void aim()
    {
        
        //check for what type of input the player is using
        foreach (InputDevice device in playerInputComponent.devices)
        {
            if(device is Keyboard)
            {
                Debug.Log("keyboard");

                //convert to vector 3
                Vector3 aimPosition = new Vector3(aimInput.x, aimInput.y, 0f);

                //get the point based off the mouse world position
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(aimPosition);
                worldPosition.z = 0f;
                
                //get the direction of which the player is pointing
                angle = worldPosition - pivot.position;
                
                //tranform it into an angle
                direction = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
                
                //rotate so it matches the angle
                transform.rotation = Quaternion.Euler(0, 0, direction);
                
            }
            else if (device is Gamepad)
            {
                Debug.Log("controller");
                Vector3 aimPosition = new Vector3(aimInput.x, aimInput.y, 0f);
                if (aimPosition.x == 0f)
                    aimPosition.x = 1f;

                //the direction for shooting is already the input of the controller
                angle = aimPosition;
                direction = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, direction);
            }
        }



    }
}
