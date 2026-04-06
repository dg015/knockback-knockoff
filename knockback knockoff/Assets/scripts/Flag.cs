using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public bool EndLevel;
    [SerializeField] private MainLevelTimer timer;
    [SerializeField] private StudioEventEmitter ambianceSound;
    [SerializeField] private StudioEventEmitter endSound;
    // Start is called before the first frame update
    void Start()
    {
       // timer = GameObject.Find("Timer").GetComponent<MainLevelTimer>();
        EndLevel = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            timer.stopTimer = true;
            if (EndLevel == false)
            {
                endSound.Play();
            }
            EndLevel = true;
            ambianceSound.Stop();
        }

    }


}
