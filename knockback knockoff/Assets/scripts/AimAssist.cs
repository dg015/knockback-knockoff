using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float targetAngle;

    [SerializeField] private Transform Area;
    [SerializeField] private float  angle;

    [SerializeField] private Rigidbody2D playerRb;
    private Vector2 lastPosition;

    [SerializeField] private float offset;

    // Start is called before the first frame update
    void Start()
    {

    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        Area.rotation = Quaternion.Euler(0, 0, angle + offset);

    }

    // Update is called once per frame
    void Update()
    {

        FindDirection();
    }
}
