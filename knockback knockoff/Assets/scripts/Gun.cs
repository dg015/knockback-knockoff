using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Vector3 rot;
    [SerializeField] private Transform pivot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        aim();
    }


    private void aim()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        float angleX = (mousePos.x  - pivot.position.x);
        float angleY = (mousePos.y - pivot.position.y);
        float direction = Mathf.Atan2(angleY, angleX) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,direction);
    }
}
