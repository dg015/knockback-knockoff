using Cinemachine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class MultiplayerScoreManager : MonoBehaviour
{

    [SerializeField] private List<PlayerController> playersControllers  = new List<PlayerController> ();
    [SerializeField] private List<GameObject> PlayerList = new List<GameObject>();
    [SerializeField] private List<int> playerScores= new List<int>();
    [SerializeField] private int maxScore;

    [SerializeField] private List<LayerMask> playerLayers;

    [SerializeField] private GameObject spawner;
    [SerializeField] private float delay;
    [SerializeField] private float currentTime;
    [SerializeField] private PlayerInputManager playerInputManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    public void addPlayerToScoreBoard(PlayerController playerController, int score,GameObject playerObject)
    {
        playersControllers.Add(playerController);
        playerScores.Add(score);
        PlayerList.Add(playerObject);
    }

    
    public void updateScore(PlayerController playerController, int score)
    {
        for (int i = 0; i < playersControllers.Count; i++)
        {
            if(playerController == playersControllers[i])
            {
                playerScores[i] = score;
            }
        }
    }


    public void processLayer(GameObject player)
    {

        Transform playerParent = player.transform.parent;
        

        //convert lyaer mask (bit) to an interger
        int layerToAdd = (int)Mathf.Log(playerLayers[PlayerList.Count - 1].value, 2);

        //set the layer
        playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        //add the layer
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        //set the action in the custom cinemachine input handler


    }

    public void endGame(int maxScore)
    {
        for (int i = 0; i < playerScores.Count; i++)
        {
            if (playerScores[i] >= maxScore)
            {
                Debug.Log("end game");
            }
        }
    }

    private void checkForDeadPlayers()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (PlayerList[i].gameObject.activeSelf == false)
            {
                //respawn
                respawnDeadPlayers(PlayerList[i].gameObject, spawner, delay);
                Debug.Log("respawn");
            }
        }
    }


    private void respawnDeadPlayers(GameObject player,GameObject spawner, float delay)
    {
        currentTime += Time.deltaTime;
        if (currentTime >= delay)
        {
            currentTime = 0;
            player.transform.position = spawner.transform.position;
            player.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkForDeadPlayers();
    }

}
