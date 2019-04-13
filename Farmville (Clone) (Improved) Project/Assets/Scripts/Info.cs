using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Info : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Button upgradeButton;
    public Button destroyButton;

    private Build build;
    private Building selectedBuilding;
    private Resources resources;

    private void Start()
    {
        resources = FindObjectOfType<Resources>();
        build = FindObjectOfType<Build>();
    }

    private void Update()
    {
        if(!build.currentSelectedGridElement && !build.currentSelectedGridElement.occupied)
        {
            nameText.text = null;
            upgradeButton.interactable = false;
            destroyButton.interactable = false;
            return;
        }

        selectedBuilding = build.currentSelectedGridElement.connectedBuilding;
        if (selectedBuilding)
        {
            destroyButton.interactable = true;
            if (resources.wood >= selectedBuilding.price.woodPrice &&
                resources.stones >= selectedBuilding.price.stonePrice &&
                resources.food >= selectedBuilding.price.foodPrice)
            {
                upgradeButton.interactable = true;
            }
            else
            {
                upgradeButton.interactable = false;
            }
            nameText.text = selectedBuilding.objectName + "\nLevel: " + selectedBuilding.buildingInformation.buildingLevel;
        }
        else
        {
            nameText.text = null;
            upgradeButton.interactable = false;
            destroyButton.interactable = false;
        }
    }

    public void OnUpgradeButton()
    {
        if(selectedBuilding)
        {
            selectedBuilding.UpgradeBuilding();
        }
    }

    public void DestroyBuildong()
    {

    }

}
