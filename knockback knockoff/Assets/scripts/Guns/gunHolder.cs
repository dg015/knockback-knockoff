using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunHolder : MonoBehaviour
{
    
    [SerializeField]public List<GameObject> weapons = new List<GameObject>();
    [SerializeField] public int selectedWeaponIndex;
    // Start is called before the first frame update
    void Start()
    {
        getWeapons();
        swapWeapons();
    }

    public void getWeapons()
    {
        weapons.Clear();
        //adds starting weapons to list
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
           // Debug.Log(selectedWeaponIndex);
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
            
            
            //Debug.Log(selectedWeaponIndex);
            swapWeapons();
        }
   
    }
    
    private void swapWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i != selectedWeaponIndex)
            {
                //weapons[i].gameObject.GetComponentInChildren<GameObject>().SetActive(false);
                weapons[i].transform.GetChild(0).gameObject.SetActive(false);

              
                if (weapons[i].gameObject.GetComponent<Gun>())
                    weapons[i].gameObject.GetComponent<Gun>().isSelected = false;
                else
                    weapons[i].gameObject.GetComponent<Leafblower>().isSelected = false;
            }
            if ( i == selectedWeaponIndex )
            {
                weapons[i].transform.GetChild(0).gameObject.SetActive(true);
                

                if (weapons[i].gameObject.GetComponent<Gun>())
                    weapons[i].gameObject.GetComponent<Gun>().isSelected = true;
                else
                    weapons[i].gameObject.GetComponent<Leafblower>().isSelected = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        getScroll();
    }
}
