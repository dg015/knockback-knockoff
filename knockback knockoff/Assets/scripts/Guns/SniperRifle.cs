using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SniperRifle : Gun
{


    [SerializeField] private bool readyToFire = true;
    // Start is called before the first frame update



    [Header("new Input system")]
    [SerializeField] private PlayerInput playerInputComponent;
    [SerializeField] private PlayerInputActions inputActions;
    void Start()
    {
        pivot = gameObject.transform.GetComponentInParent<Transform>();
        playerRb = gameObject.transform.GetComponentInParent<Rigidbody2D>();

        barrel = transform.GetChild(1);
        controller = gameObject.transform.GetComponentInParent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        aim();
        CameraShakeTimer();
        RunningTime += Time.deltaTime;
        //shootingDelay();
        
        /*
        if (Input.GetMouseButton(0) && readyToFire)
        {
            StartCoroutine(shootingCycle());
        }
        */
    }
}
