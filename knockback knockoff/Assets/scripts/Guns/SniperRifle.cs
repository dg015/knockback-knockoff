using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SniperRifle : Gun
{

    [SerializeField] private CinemachineVirtualCamera Cinemachine;
    [SerializeField] private CinemachineBasicMultiChannelPerlin shake;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float Shaketime;
    [SerializeField] private float shaketimeMax;
    [SerializeField] private bool readyToFire = true;
    // Start is called before the first frame update

    void Start()
    {
        pivot = gameObject.transform.GetComponentInParent<Transform>();
        playerRb = gameObject.transform.GetComponentInParent<Rigidbody2D>();
        Cinemachine = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        shake = Cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        barrel = transform.GetChild(1);
        controller = gameObject.transform.GetComponentInParent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        aim();
        CameraShakeTimer();
        RunningTime += Time.deltaTime;
        shootingDelay();
        
        if (Input.GetMouseButton(0) && readyToFire)
        {
            StartCoroutine(shootingCycle());
        }

    }


    public void CameraShake()
    {
        //add camera shake 
        shake.m_AmplitudeGain = shakeIntensity;
    }

    private void CameraShakeTimer()
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

    protected override void shoot()
    {
        base.shoot();
        CameraShake();
    }

    private IEnumerator shootingCycle()
    {

        shoot();
        readyToFire = false;
        yield return new WaitForSeconds(timeBetweenShots);
        Debug.Log("I'me here");
        readyToFire = true;


    }
}
