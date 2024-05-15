using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinScore;
    public static int MaxCoin;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(MaxCoin);
    }


    public void addCoinScore()
    {
        coinScore++;

    }

    public void remoceCoinScore()
    {
        coinScore--;
    }

    public void endGameCoinChecker()
    {
        if (coinScore == MaxCoin)
        {
            Debug.Log("end game");
        }
    }
    // Update is called once per frame
    void Update()
    {
        endGameCoinChecker();
        
    }
}
