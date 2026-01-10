using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class fireballKill : MonoBehaviour
{
    [SerializeField] private MultiplayerScoreManager scoreManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SpeedChecker speedChecker;
    [SerializeField] private Collider2D owner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = GameObject.Find("Game manager").GetComponent<MultiplayerScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.GetComponent<PlayerController>() != null && collision != owner)
        {

            if (speedChecker.KillSpeed == true)
            {

                scoreManager.updateScore(playerController, playerController.score);
                scoreManager.endGame(playerController.score);
                //disable it is easier then destroying
                collision.gameObject.GetComponent<PlayerController>().alive = false;
                collision.gameObject.SetActive(false);

                playerController.score++;
                Debug.Log("killed player");
            }

        }
    }

}
