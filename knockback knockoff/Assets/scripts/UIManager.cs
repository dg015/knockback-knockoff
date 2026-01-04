using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> UIElments;

    [SerializeField] private GameObject leafblowerObject;
    [SerializeField] private Leafblower leafblower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //UIElments = new List<Image>();

        //leafblowerObject = transform.Find("Leafblower").gameObject;
        //leafblower = leafblowerObject.GetComponent<Leafblower>();

    }

    // Update is called once per frame
    void Update()
    {
        enableImages(leafblowerObject, UIElments);
        disableImages(leafblowerObject, UIElments);

        fillBar(leafblower.heat,leafblower.overheatMaximum,leafblower.minimumHeat);
    }

    private void enableImages(GameObject gun, List<GameObject> images)
    {
        if (gun.activeSelf == true)
        {
            Debug.Log("gun active");
            for (int i = 0; i < UIElments.Count; i++)
            {
                Debug.Log(i);
                if (i == 0 || i == 1)
                {
                    Debug.Log(UIElments[i]);
                    UIElments[i].SetActive(true);
                }
            }
        }
    }

    private void disableImages(GameObject gun, List<GameObject> images)
    {
        if (gun.activeSelf == false)
        {
            //disable images
            foreach (GameObject image in images)
            {
                Debug.Log("disabled images");
                image.SetActive(false);
            }

        }
    }


    public void fillBar(float gunHeat, float valueMax, float valueMin)
    {
        //normalzing
        float finalValue = (gunHeat - valueMin) / (valueMax - valueMin);
        Image UIBar = UIElments[1].GetComponent<Image>();
        UIBar.fillAmount = finalValue;
    }
}
