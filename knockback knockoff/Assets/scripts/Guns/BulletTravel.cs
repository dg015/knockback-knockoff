using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class BulletTravel : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float maxTime;
    private bool destructable = false;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer();
        rb.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
    }


    private void countdownTimer()
    {

        time += Time.deltaTime;
        if (time > maxTime)
        {
            destructable = true;
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("bullet"))
        {
            if  (destructable == true)
            {
                Debug.Log("destroyed by bullet");
                Destroy(this.gameObject);
                
            }
        }
        else if (collision.CompareTag("helper"))
        {

        }
        else
        {
            Debug.Log(collision);
            Debug.Log("destroyed");
            Destroy(this.gameObject);
            
        }

    }
}
