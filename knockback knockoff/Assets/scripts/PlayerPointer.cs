using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.FilePathAttribute;


public class PlayerPointer : MonoBehaviour
{
    [SerializeField] PlayerController[] pLocation;
    [SerializeField] private Canvas pointer;
    // Start is called before the first frame update
    void Start()
    {
        pLocation = GameObject.FindObjectsOfType<PlayerController>(); 
        Transform thisPlayerTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Find();
        //pointer.transform.rotation = 
    }

    private void Find()
    {
        Transform thisPlayerTransform = transform;
        foreach (PlayerController player in pLocation)
        {
            // If the player is not the player this script is attached to
            if (player.transform != thisPlayerTransform)
            {
                // Get the position of the player
                Vector3 playerPosition = player.transform.position;
                // Log the position of the player
                Debug.Log("Player at position: " + playerPosition);

            }
        }

        /*
        PlayerLocations = new List<Transform> ();
        for (int i = 0; i< PlayerController.PlayerCount; i++)
        {
            PlayerLocations.Add()
            PlayerLocations[i] = gameObject.transform;

        }
        */
    }
}
