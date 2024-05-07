using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Vector3 location;
    public BoxCollider2D bc;
    public Rigidbody2D rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private float velocityMax;
    [SerializeField] private float jumpForce;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask ground;

    public static int PlayerCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    protected void Awake()
    {
        PlayerCount++;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rb.velocity);
        isGrounded();
        location = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        movement();
    }

    private bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance,ground))
        {
            return true;
        }
        else 
        {
            return false; 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void movement()
    {

        float Hspeed = Input.GetAxis("Horizontal") * speed;

        if (Input.GetAxis("Horizontal") > -1.1)
        {

            rb.AddForce(new Vector2(Hspeed * speed, 0f));

        }
        if (isGrounded())
        {
            // sets the velocity based on a vector3 the clampsthe value inputed wihch is transformed into lenght only.
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 15);
            if (Input.GetKey(KeyCode.Space))
            {
                

                rb.AddForce(new Vector2(0, jumpForce) * speed * Time.deltaTime, ForceMode2D.Impulse);
                
            }
        }
    }



}
