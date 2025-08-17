using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteArmControl : MonoBehaviour
{
    [SerializeField] private GameObject arm;
    
    [SerializeField] private GameObject[] twoHandedWeapons;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(checkForTwoHandedWeapons())
        {
            hideArm();
        }
        else
        {
            revealArm();
        }
    }

    public bool checkForTwoHandedWeapons()
    {
        //Runs trough all children of weaponHolder
        foreach (Transform Child in transform)
        {
            //checks for active gameobjects
            if(Child.gameObject.activeInHierarchy)
            {
                //runs though all gameobjects in array
                foreach(GameObject prefab in twoHandedWeapons)
                {
                    //if it starts with the same name then return true
                    // Im doing this because the prefab is always being instantiated with the word (clone) in front of it
                    if( Child.name.StartsWith(prefab.name) )
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void hideArm()
    {
        arm.gameObject.SetActive(false);
    }

    private void revealArm()
    {
        arm.gameObject.SetActive(true);
    }
}
