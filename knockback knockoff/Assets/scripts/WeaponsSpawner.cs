using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private List<GameObject> Weapons;


    //NEED TO MAKE IT INSTANTIATE ONLY ONCE, RN ITS GOING TO INSTANTIATE A LOT OF OBJECTS AT ONCE
    //Gun 1: pistol
    //Gun 2: leafblower
    //Gun 3: Sniper rifle
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

   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Transform PlayerTransform = collision.transform; // Assuming all weapon objects are children of the player
            Transform gunHolderTransform = collision.transform.Find("Gun holder");
            gunHolder gunHolderScript = collision.GetComponentInChildren<gunHolder>();
            //I have to create a new script to add the singular weapon
            if (GunIndex == 0)
            {
                if (!HasComponentInChildren<Gun>(PlayerTransform))
                {
                    GameObject currentSelectedGun;
                    currentSelectedGun = gunHolderScript.weapons[gunHolderScript.selectedWeaponIndex];
                    currentSelectedGun.SetActive(false);

                    Debug.Log("doesn't have pistol");
                    Instantiate(Weapons[GunIndex], gunHolderTransform);
                    gunHolderScript.weapons.Add(Weapons[GunIndex]);
                    gunHolderScript.getWeapons();
                    
                    //gunHolderScript.selectedWeaponIndex = gunHolderScript.weapons.Count-1;
                }
            }
            else if (GunIndex == 1)
            {
                if (!HasComponentInChildren<Leafblower>(PlayerTransform))
                {
                    GameObject currentSelectedGun;
                    currentSelectedGun = gunHolderScript.weapons[gunHolderScript.selectedWeaponIndex];
                    currentSelectedGun.SetActive(false);

                    Debug.Log("doesn't have leaf blower");
                    Instantiate(Weapons[GunIndex], gunHolderTransform);
                    gunHolderScript.weapons.Add(Weapons[GunIndex]);
                    gunHolderScript.getWeapons();
                    //gunHolderScript.selectedWeaponIndex = gunHolderScript.weapons.Count - 1;
                }
            }
            else if (GunIndex == 2)
            {
                if (!HasComponentInChildren<SniperRifle>(PlayerTransform))
                {
                    GameObject currentSelectedGun;
                    currentSelectedGun = gunHolderScript.weapons[gunHolderScript.selectedWeaponIndex];
                    currentSelectedGun.SetActive(false);

                    Debug.Log("doesn't have sniper rifle");
                    Instantiate(Weapons[GunIndex], gunHolderTransform);
                    gunHolderScript.weapons.Add(Weapons[GunIndex]);
                    gunHolderScript.getWeapons();
                    //gunHolderScript.selectedWeaponIndex = gunHolderScript.weapons.Count - 1;
                }
            }
            else
            {
                Debug.Log("has spawned weapon");
            }
        }
    }

    private bool HasComponentInChildren<T>(Transform parent) where T : Component
    {
        foreach (Transform child in parent)
        {
            T component = child.GetComponent<T>();
            if (component != null)
            {
                return true;
            }

            if (HasComponentInChildren<T>(child))
            {
                return true;
            }
        }

        return false;
    }
}
