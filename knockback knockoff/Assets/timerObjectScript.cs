using UnityEngine;

public class timerObjectScript : MonoBehaviour
{
    [SerializeField] private MainLevelTimer timerManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerManager = FindAnyObjectByType<MainLevelTimer>().GetComponent<MainLevelTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        timerManager.setText(this.gameObject);
    }
}
