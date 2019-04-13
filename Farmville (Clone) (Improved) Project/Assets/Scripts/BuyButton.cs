using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public int connectedBuildingID;
    public TextMeshProUGUI resourcesRequirementText;

    private Building connectedBuilding;
    private Button button;
    private Resources resources;

    private void Start()
    {
        resources = FindObjectOfType<Resources>();
        button = GetComponent<Button>();

        Buildings buildings = FindObjectOfType<Buildings>();
        foreach(GameObject g in buildings.buildableObjects)
        {
            if(g.GetComponent<Building>().buildingInformation.buildingID == connectedBuildingID)
            {
                connectedBuilding = g.GetComponent<Building>();
                break;
            }
        }

        resourcesRequirementText.text = connectedBuilding.price.woodPrice  + "Wo. | " +
            connectedBuilding.price.stonePrice + "St. | " + connectedBuilding.price.foodPrice + "Fo.";
    }

    private void Update()
    {
        if(resources.wood >= connectedBuilding.price.woodPrice &&
           resources.stones >= connectedBuilding.price.stonePrice &&
           resources.food >= connectedBuilding.price.foodPrice)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
