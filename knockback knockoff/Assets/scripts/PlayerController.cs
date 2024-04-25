using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BoxCollider2D bc;
    public Rigidbody2D rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private float velocityMax;
    [SerializeField] private float jumpForce;
    [SerializeField] private float colisions;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rb.velocity);

    }

    private void FixedUpdate()
    {
        movement();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        colisions++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        colisions--;
    }

    private void movement()
    {

        float Hspeed = Input.GetAxis("Horizontal") * speed;

        if (Input.GetAxis("Horizontal") > -1.1)
        {

            rb.AddForce(new Vector2(Hspeed * speed, 0f));

        }
        if (colisions > 0)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 15);
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("jump");

                rb.AddForce(new Vector2(0, jumpForce) * speed * Time.deltaTime, ForceMode2D.Impulse);
                Debug.Log("pressed");
            }
        }
        // sets the velocity based on a vector3 the clampsthe value inputed wihch is transformed into lenght only.

    }



}
