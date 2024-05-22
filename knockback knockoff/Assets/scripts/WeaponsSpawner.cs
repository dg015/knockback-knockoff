using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSpawner : MonoBehaviour
{
    [SerializeField] public int GunIndex;
    [SerializeField] public float GunSpawnerTimer;
    [SerializeField] public List<GameObject> GunTexture;
    [SerializeField] private bool readyToSpawn;
    private float timer;

    //NEED TO MAKE IT INSTANTIATE ONLY ONCE, RN ITS GOING TO INSTANTIATE A LOT OF OBJECTS AT ONCE

    private void getGunIndex()
    {
        //Instantiate a child game object that has the texture of the gun the player is getting
        if (readyToSpawn)
        {
            Instantiate(GunTexture[GunIndex],transform);
        }
    }

    private void GunSpawnerCountdown()
    {
        if (timer < GunSpawnerTimer)
        {
            timer += Time.deltaTime;
        }
        else if (timer == GunSpawnerTimer)
        {
            readyToSpawn = true;
        }
        else
        {
            readyToSpawn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GunSpawnerCountdown();
        getGunIndex();
    }
}
