using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    //spawn
    [SerializeField] protected Transform pivot;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected Transform barrel;

    // knockback
    [SerializeField] protected Rigidbody2D playerRb;
    [SerializeField] public float force;

    protected Vector2 angle;
    protected float direction;

    [SerializeField] protected PlayerController controller;

    //shooting
    [SerializeField] protected float timeBetweenShots;
    [SerializeField] protected float RunningTime;
    


    // Start is called before the first frame update
    void Start()
    {
        
        pivot = gameObject.transform.GetComponentInParent<Transform>();
        playerRb = gameObject.transform.GetComponentInParent<Rigidbody2D>();
        controller = gameObject.transform.GetComponentInParent<PlayerController>();
        barrel = GameObject.Find("barrel").GetComponent<Transform>();
        //barrel = gameObject.transform.GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        RunningTime += Time.deltaTime;
        aim();
        shootingDelay();
    }

    protected void shootingDelay()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (RunningTime >= timeBetweenShots)
            {
                RunningTime = 0;
                shoot();
            }
        }
    }

    protected virtual void shoot()
    {

        //playerRb.AddForce(-1 * angle * force, ForceMode2D.Force);
        Vector2 direction = new Vector2();
        direction = -angle.normalized;
        //Debug.Log(direction);
        controller.PVelocity = direction * force;


        Instantiate(bullet, barrel.position, barrel.rotation);
    }


    protected void aim()
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
