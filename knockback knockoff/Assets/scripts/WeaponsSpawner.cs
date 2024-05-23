using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponsSpawner : MonoBehaviour
{
    [SerializeField] public int GunIndex;
    [SerializeField] public float GunSpawnerTimer;
    [SerializeField] public List<GameObject> GunTexture;
    [SerializeField] private bool readyToSpawn;
    [SerializeField] private bool weaponSpawned;
    [SerializeField] private float timer;

    //NEED TO MAKE IT INSTANTIATE ONLY ONCE, RN ITS GOING TO INSTANTIATE A LOT OF OBJECTS AT ONCE

    private void getGunIndex()
    {
        //Instantiate a child game object that has the texture of the gun the player is getting
        if (readyToSpawn)
        {
            if (!weaponSpawned)
            {
                Instantiate(GunTexture[GunIndex],new Vector2(transform.position.x, transform.position.y + 1.25f),transform.rotation);
                weaponSpawned = true;
            }
        }
    }

    private void GunSpawnerCountdown()
    {
        if (timer < GunSpawnerTimer)
        {
            if (readyToSpawn == false)
            {
                timer += Time.deltaTime;
            }
        }
        else if (timer >= GunSpawnerTimer)
        {
            readyToSpawn = true;
            timer = 0;
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
