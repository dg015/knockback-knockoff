using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinManager manager;

   public void MaxCoinCount()
    {
        CoinManager.MaxCoin++;

    }
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Coin manager").GetComponent<CoinManager>();
        MaxCoinCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        manager.addCoinScore();
    }
}
