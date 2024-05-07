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
    [SerializeField] private Transform barrel;
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
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            shoot();
              
        }
    }


    private void shoot()
    {
        playerRb.AddForce(-1 * angle * force, ForceMode2D.Force);
        Instantiate(bullet, barrel.position, barrel.rotation);
            
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
