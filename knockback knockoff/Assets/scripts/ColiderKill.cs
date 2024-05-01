using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderKill : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D col2d;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float force;
    [SerializeField] private Transform target;
    [SerializeField] private float targetAngle;
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
            
        }*/
    }


    private float angle()
    {
        return targetAngle;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
