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


    // Start is called before the first frame update
    void Start()
    {
  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.transform;
            inRange = true;

        }
     /*
        if (collision.CompareTag("Player"))
        {
            Transform targetPosition = collision.GetComponent<Transform>();
            Vector2 targetDirection = new Vector2((targetPosition.position.x - transform.position.x), (targetPosition.position.y - transform.position.y));
            float direction = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            //might go really wrong
            float angleDifference = 
            if ( direction != playerRb.velocity)
            {

            }

            //transform.rotation = Quaternion.Euler(0, 0, direction);
            
        }
     */
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

        Vector2 distance = (transform.position - target.position).normalized;
        /*
        Targetangle = new Vector2((transform.position.x - target.position.x), (transform.position.y - target.position.y));
        float angleDistance = Mathf.Atan2(Targetangle.y, Targetangle.x) * Mathf.Rad2Deg;
        */
        playerRb.AddForce(distance * strenght);
        Debug.Log(distance);
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
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        
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
