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

    [SerializeField] private bool alreadyHasWeapon;

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
        bool alreadyHasWeapon = false;
        if (collision.CompareTag("Player"))
        {
            Transform PlayerTransform = collision.transform; // Assuming all weapon objects are children of the player

            
            Transform gunHolderTransform = collision.transform.Find("Gun holder");
            gunHolder gunHolderScript = collision.GetComponentInChildren<gunHolder>();

            //Gun gun;
            //gun = collision.GetComponentInChildren<Gun>();

            Gun selectedWeapon = Weapons[GunIndex].GetComponent<Gun>();
            foreach (GameObject gunObj in gunHolderScript.weapons)
            {
                //Debug.Log(selectedWeapon.gunID);
                Gun weapon = gunObj.GetComponent<Gun>();
                Debug.Log(weapon.gunID);
                if (weapon.gunID == selectedWeapon.gunID)
                {
                    alreadyHasWeapon = true;
                    break;
                }

            }
            if (!alreadyHasWeapon) // has to be called outside of foreach otherwise will change the list while its being read by foreach
            {

                //get current weapon
                GameObject currentSelectedGun;
                currentSelectedGun = gunHolderScript.weapons[gunHolderScript.selectedWeaponIndex];
                currentSelectedGun.SetActive(false);

                //add selected weapons
                GameObject newGun = Instantiate(Weapons[GunIndex], gunHolderTransform);
                gunHolderScript.weapons.Add(newGun);
                gunHolderScript.getWeapons();

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
