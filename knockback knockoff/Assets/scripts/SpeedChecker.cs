using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedChecker : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Image speedBar;
    [SerializeField] private float velocity;
    [SerializeField] private bool maxSpeedReached;

    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask player;
    [SerializeField] private float maxspeed;
    // Start is called before the first frame update
    void Start()
    {
        rb= gameObject.GetComponent<Rigidbody2D>();
        speedBar = GameObject.Find("bar fill").GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude/ 20;
        fill();
        killSpeed();
        //speedColision();
    }

    private void killSpeed()
    {
        if(rb.velocity.magnitude / maxspeed > 1)
        {
            maxSpeedReached = true;

        }
        else
        {
            maxSpeedReached = false;
        }


    }

    /*
    private void speedColision()
    {
        BoxCollider2D col = this.gameObject.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(gameObject.transform, col);
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, player))
        {
            if (maxSpeed)
            {
                Debug.Log("kill");
            }
        }

    }
    */


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.collider.CompareTag("Player"))
        {
            if (maxSpeedReached)
            {
                Destroy(collision.gameObject);
                Debug.Log("kill");
            }
        }
    }

    

    private void fill()
    {
        speedBar.fillAmount = rb.velocity.magnitude / maxspeed;



    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
