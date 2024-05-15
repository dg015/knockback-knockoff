using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float targetAngle;
    [SerializeField] private float strenght;

    [SerializeField] private Transform Area;
    [SerializeField] private Vector2  Targetangle;

    [SerializeField] private bool inRange = false;
    [SerializeField] private Rigidbody2D playerRb;
    private Vector2 lastPosition;

    public float angle;

    [SerializeField] private float RequiredSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerRb.velocity.magnitude > RequiredSpeed)
            {
                target = collision.transform;
                inRange = true;
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void assist()
    {
        //get distance
        Vector2 distance = (transform.position - target.position).normalized;

        //add force to warlk towards the distance vector between target and player
        playerRb.AddForce(distance * strenght);
        
    }

    private void FindDirection()
    {
        // Calculate the current velocity of the player
        Vector2 currentPosition = playerRb.position;
        Vector2 currentVelocity = (currentPosition - lastPosition) / Time.deltaTime;
        lastPosition = currentPosition;

        // Determine the direction of movement based on velocity
        Vector2 movementDirection = currentVelocity.normalized;
   



        // Calculate the angle of movement
         angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        
        // Apply the rotation to the "Area" transform
        Area.rotation = Quaternion.Euler(0, 0, angle -90 );




    }

    // Update is called once per frame
    void Update()
    {
        
        FindDirection();
        if (inRange)
        {
            assist();
        }

    }
}
