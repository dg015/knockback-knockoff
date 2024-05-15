using System.Collections;
using System.Collections.Generic;
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

    //shooting
    [SerializeField] protected float timeBetweenShots;
    [SerializeField] protected float RunningTime;
    


    // Start is called before the first frame update
    void Start()
    {
        
        pivot = gameObject.transform.GetComponentInParent<Transform>();
        playerRb = gameObject.transform.GetComponentInParent<Rigidbody2D>();
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
        
        playerRb.AddForce(-1 * angle * force, ForceMode2D.Force);
        Instantiate(bullet, barrel.position, barrel.rotation);
    }


    protected void aim()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        angle = new Vector2 ((mousePos.x - pivot.position.x),(mousePos.y - pivot.position.y));
        direction = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,direction);
    }
}
