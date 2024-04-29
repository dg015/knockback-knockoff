using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float force;
    private Vector2 angle;
    private float direction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        aim();
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            shoot();
            Debug.Log("shot");
        }
    }

    private void shoot()
    {
        playerRb.AddForce(-1 * angle * force, ForceMode2D.Force);


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
