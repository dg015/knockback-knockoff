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
    PlayerController closestPlayer = null;
    float closestDistanceSqr;
    // Start is called before the first frame update
    void Start()
    {
        pLocation = GameObject.FindObjectsOfType<PlayerController>(); 
        Transform thisPlayerTransform = transform;
        closestDistanceSqr = Mathf.Infinity;
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
                Vector3 playerPosition = this.transform.position;

                // Log the position of the player
                Debug.Log("Player at position: " + playerPosition);

                float sqrDistanceToPlayer = (player.transform.position - thisPlayerTransform.position).magnitude;
                if (sqrDistanceToPlayer < closestDistanceSqr)
                {
                    closestPlayer = player;
                    closestDistanceSqr = sqrDistanceToPlayer;
                }

            }
        }
        if (closestPlayer != null)
        {
            Vector3 closestPlayerPosition = closestPlayer.transform.position;
            Debug.Log("Closest player at position: " + closestPlayerPosition);
        }
        else
        {
            Debug.Log("No other players found in the scene.");
        }
    }
}
