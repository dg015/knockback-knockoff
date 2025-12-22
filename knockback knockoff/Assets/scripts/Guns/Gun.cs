using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
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

    [Header("Shooting")]
    [SerializeField] protected float timeBetweenShots;
    [SerializeField] protected float RunningTime;
    [SerializeField] protected bool readyToFire;

    [Header("Shake")]
    [SerializeField] protected CinemachineVirtualCamera Cinemachine;
    [SerializeField] protected CinemachineBasicMultiChannelPerlin shake;
    [SerializeField] protected float shakeIntensity;
    [SerializeField] protected float Shaketime;
    [SerializeField] protected float shaketimeMax;

    [Header("new Input system")]
    [SerializeField] private PlayerInput playerInputComponent;
    [SerializeField] private PlayerInputActions inputActions;

    // Start is called before the first frame update



    private void OnEnable()
    {
        PlayerInputActions inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Shoot.performed += callShootMethod;

        //inputActions.Player.Jump.performed += ;
    }

    private void OnDisable()
    {
        inputActions.Player.Shoot.performed -= callShootMethod;
        inputActions.Player.Disable();
        //inputActions.Player.Jump.performed -= ;
    }

    void Start()
    {
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
        //shootingDelay();
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
    
    /*
    public void shootingDelay(InputAction.CallbackContext context)
    {
        Debug.Log("trying to shoot");
            if (RunningTime >= timeBetweenShots)
            {
                RunningTime = 0;
                shoot();
                CameraShake();
            }
        
    }
    */

    public void callShootMethod(InputAction.CallbackContext context)
    {
        if (readyToFire)
        StartCoroutine(shootingCycle());
    }

    protected IEnumerator shootingCycle()
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
