using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    //spawn
    [SerializeField] private Transform pivot;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrel;

    // knockback
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] public float force;

    private Vector2 angle;
    private float direction;

    //shooting
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float RunningTime;
    


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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (RunningTime >= timeBetweenShots)
            {
                RunningTime = 0;
                shoot();
            }

              
        }
    }


    private void shoot()
    {
        
        playerRb.AddForce(-1 * angle * force, ForceMode2D.Force);
        Instantiate(bullet, barrel.position, barrel.rotation);
    }


    private void aim()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        angle = new Vector2 ((mousePos.x - pivot.position.x),(mousePos.y - pivot.position.y));
        direction = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,direction);
    }
}
