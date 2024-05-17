using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunHolder : MonoBehaviour
{
    private int numberOfGuns;
    [SerializeField] List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private int selectedWeaponIndex;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(selectedWeaponIndex);
        getWeapons();

    }

    private void getWeapons()
    {
        
        foreach (Transform child in transform)

        {
           weapons.Add(child.gameObject);
        }
    }

    private void getScroll()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0.1)
        {
            if (selectedWeaponIndex >= weapons.Count -1 )
            {
                selectedWeaponIndex = 0;
            }
            else
            {
                selectedWeaponIndex++;
            }
            Debug.Log(selectedWeaponIndex);
            swapWeapons();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < -0.1)
        {
            if (selectedWeaponIndex <= 0)
            {
                selectedWeaponIndex = weapons.Count - 1;
            }
            else
            {
                selectedWeaponIndex--;
            }
            
            Debug.Log(selectedWeaponIndex);
            swapWeapons();
        }
   
    }
    
    private void swapWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i != selectedWeaponIndex)
            {
                weapons[i].gameObject.SetActive(false);
            }
            if ( i == selectedWeaponIndex )
            {
                weapons[selectedWeaponIndex].gameObject.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        getScroll();
    }
}
