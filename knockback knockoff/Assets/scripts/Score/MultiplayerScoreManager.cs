using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MultiplayerScoreManager : MonoBehaviour
{

    [SerializeField] private List<PlayerController> playersControllers  = new List<PlayerController> ();
    [SerializeField] private List<int> playerScores= new List<int>();
    [SerializeField] private int maxScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void addPlayerToScoreBoard(PlayerController playerController, int score)
    {
        playersControllers.Add(playerController);
        playerScores.Add(score);
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


    // Update is called once per frame
    void Update()
    {

    }

}
