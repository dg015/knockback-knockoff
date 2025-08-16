using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookAt : MonoBehaviour
{
    [SerializeField] private Gun gunScript;
    // Start is called before the first frame update
    void Start()
    {
        gunScript = GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        gunScript.aim();
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
}
