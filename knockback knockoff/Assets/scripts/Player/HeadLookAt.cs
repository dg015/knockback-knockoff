using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookAt : MonoBehaviour
{
    private Vector2 angle;
    private float direction;

    [SerializeField] protected Transform pivot;

    // Update is called once per frame
    void Update()
    {
        lookAt();
        mirrorHead();
    }
    private void mirrorHead()
    {
        float rotationZ = transform.localEulerAngles.z;

        if (rotationZ > 90 && rotationZ < 270)
        {
            // Facing left
            transform.localScale = new Vector3(1, -1, -1);
        }
        else
        {
            // Facing right
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void lookAt()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        angle = new Vector2((mousePos.x - pivot.position.x), (mousePos.y - pivot.position.y));
        direction = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, direction);

    }

}
