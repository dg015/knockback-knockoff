using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leafblower : Gun
{
    [SerializeField] private float overheatMaximum;
    [SerializeField] private float heat;
    [SerializeField] private bool readyToFire = true;
    [SerializeField] private float minimumHeat;

    [SerializeField] private float StartUpHeat;
    // Start is called before the first frame update
    void Start()
    {
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
        RunningTime += Time.deltaTime;
    }

    private void firing()
    {
        if (readyToFire)
        {

            if (Input.GetMouseButton(0))
            {
                RunningTime = 0;
                heat += Time.deltaTime;
                shoot();
            }

        }

    }

    protected virtual void shooting()
    {
        
        //playerRb.AddForce(-1 * angle * force, ForceMode2D.Force);
        Vector2 direction = new Vector2();
        direction = -angle.normalized;
        Debug.Log(direction);
        controller.PVelocity = direction * force;


        Instantiate(bullet, barrel.position, barrel.rotation);
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
        if (!Input.GetMouseButton(0) && RunningTime >timeBetweenShots)
        {
            
            heat -= Time.deltaTime;
        }
        
    }
}
