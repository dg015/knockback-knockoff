using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class gunHolder : MonoBehaviour
{
    
    [SerializeField] public List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private List<Gun> gunList = new List<Gun>();
    [SerializeField] public int selectedWeaponIndex;

    [SerializeField] private PlayerInput playerInputComponent;


    [SerializeField] private Vector2 scrollValue;
    [SerializeField] private float buttonValue;

    [SerializeField] private Vector2 aimValue;


    [SerializeField] private float triggerInput;

    // Start is called before the first frame update
    void Start()
    {
        getWeapons();
        swapWeapons();
    }

    public void getWeapons()
    {
        weapons.Clear();
        gunList.Clear();
        //adds starting weapons to list
        foreach (Transform child in transform)
        {
            
           weapons.Add(child.gameObject);
           gunList.Add(child.gameObject.GetComponent<Gun>());
        }
    }


    public void callShootMethod(InputAction.CallbackContext context)
    {
        triggerInput = context.ReadValue<float>();


        if (context.started)
        {
            if (triggerInput > 0.25f && gunList[selectedWeaponIndex].readyToFire)
            {
                //Debug.Log("started action");
                gunList[selectedWeaponIndex].isShooting = true;
            }
        }
        else if (context.performed)
        {
            if (triggerInput > 0.25f && gunList[selectedWeaponIndex].readyToFire)
            {
                // Debug.Log("continuing action");
                gunList[selectedWeaponIndex].isShooting = true;
            }
        }
        else if (context.canceled)
        {
            if (triggerInput < 0.25f)
            {
                //Debug.Log("Button released");
                gunList[selectedWeaponIndex].isShooting = false;
            }
        }

    }


    public void onAim(InputAction.CallbackContext context)
    {
        aimValue = context.ReadValue<Vector2>();
    }

    public void GetButtonChange(InputAction.CallbackContext context)
    {
        buttonValue = context.ReadValue<float>();
        if (context.performed)
        {
            if (buttonValue > 0.1)
            {
                if (selectedWeaponIndex >= weapons.Count - 1)
                {

                    selectedWeaponIndex = 0;
                }
                else
                {
                    selectedWeaponIndex++;
                }
            }
            if (buttonValue < -0.1)
            {
                if (selectedWeaponIndex <= 0)
                {
                    selectedWeaponIndex = weapons.Count - 1;
                }
                else
                {
                    selectedWeaponIndex--;
                }
            }
            swapWeapons();
        }
    }

    public void getScroll(InputAction.CallbackContext context)
    {

       scrollValue = context.ReadValue<Vector2>();
            
        if (context.performed)
        {
            if (scrollValue.y > 0.1)
            {
                if (selectedWeaponIndex >= weapons.Count - 1)
                {

                    selectedWeaponIndex = 0;
                }
                else
                {
                    selectedWeaponIndex++;
                }
            }
            if (scrollValue.y < -0.1)
            {
                if (selectedWeaponIndex <= 0)
                {
                    selectedWeaponIndex = weapons.Count - 1;
                }
                else
                {
                    selectedWeaponIndex--;
                }
            }
            swapWeapons();
        }
    }
    
    private void swapWeapons()
    {
        Debug.Log("swapping weapons");
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

                Debug.Log(weapons[i].transform.GetChild(0).name.ToString());
                if (weapons[i].gameObject.GetComponent<Gun>())
                    weapons[i].gameObject.GetComponent<Gun>().isSelected = true;
                else
                    weapons[i].gameObject.GetComponent<Leafblower>().isSelected = true;
            }
        }
    }



    private void addWeaponsToInput()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        gunList[selectedWeaponIndex].aimInput = aimValue;

        
    }
}
