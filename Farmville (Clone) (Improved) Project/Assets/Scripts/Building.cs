using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Price
{
    public int woodPrice;
    public int stonePrice;
    public int foodPrice;
}

[System.Serializable]
public class BuildingInformation
{
    public int buildingLevel = 0;
    public int buildingID;
    public float yRotation;
    public int connectedGridID;
}

public class Building: MonoBehaviour
{
    public Price price;
    public BuildingInformation buildingInformation;
    public string objectName;
    public bool placed;
    public int baseResourceGain = 1;

    private Resources resources;

    private void Awake()
    {
        resources = FindObjectOfType<Resources>();
    }

    private void Update()
    {
        if(!placed)
        {
            return;
        }

        switch (buildingInformation.buildingID)
        {
            case 0:
                resources.wood += baseResourceGain * buildingInformation.buildingLevel * Time.deltaTime;
                break;
            case 1:
                resources.stones += baseResourceGain * buildingInformation.buildingLevel * Time.deltaTime;
                break;
            case 2  :
                resources.food += baseResourceGain * buildingInformation.buildingLevel * Time.deltaTime;
                break; 
        }
    }

    public void UpgradeBuilding()
    {
        buildingInformation.buildingLevel++;
        resources.wood -= price.woodPrice;
        resources.stones -= price.stonePrice;
        resources.food -= price.foodPrice;
    }
}
