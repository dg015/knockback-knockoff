using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpriteRotation : MonoBehaviour
{

    [SerializeField] private bool head;
    [SerializeField] private Transform gun;
    // Update is called once per frame
    void Update()
    {
        
        float rotationZ = transform.parent.localEulerAngles.z;
            if (rotationZ > 90 && rotationZ < 270)
            {
                // Facing left
                transform.localScale = new Vector3(-1, -1, 1);
            }
            else
            {
                // Facing right
                transform.localScale = new Vector3(-1, 1, 1);
            }
        

    }
}
