using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.FilePathAttribute;


public class PlayerPointer : MonoBehaviour
{
    [SerializeField] PlayerController[] pLocation;
    [SerializeField] private Canvas pointerCanvas;
    PlayerController closestPlayer = null;
    float closestDistanceSqr;




    // Start is called before the first frame update
    void Start()
    {
        pointerCanvas = gameObject.transform.GetComponentInChildren<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {
        Find();
        //pointer.transform.rotation = 
    }

    private void Find()
    {


        //pLocation = GameObject.FindObjectsOfType<PlayerController>();
        closestDistanceSqr = Mathf.Infinity;
        Transform thisPlayerTransform = transform;
        foreach (PlayerController player in pLocation)
        {
            // If the player is not the player this script is attached to
            if (player.transform != thisPlayerTransform)
            {
                // Get the position of the player
                Vector3 playerPosition = this.transform.position;

                // Log the position of the player
                

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
            Vector2 closestPlayerPosition = closestPlayer.transform.position;
            


            Vector3 directionToClosestPlayer = closestPlayer.transform.position - pointerCanvas.transform.position;
            float angle = Mathf.Atan2(directionToClosestPlayer.y, directionToClosestPlayer.x) * Mathf.Rad2Deg;
            pointerCanvas.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));



        }
        else
        {
            // Debug.Log("No other players found in the scene.");
        }

    }
}
