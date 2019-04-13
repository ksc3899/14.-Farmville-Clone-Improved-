using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GridElement currentSelectedGridElement;
    public GridElement currentHoveringGridElement;
    public Buildings buildings;

    [Header("Colors")]
    public Color colorOnHover = Color.blue;
    public Color colorOnOccupied = Color.red;

    [Header("Private Variables")]
    [SerializeField] public GridElement[] grid = new GridElement[36];
    [SerializeField] private Resources resources;
    [SerializeField] private bool buildInProgress;
    [SerializeField] private GameObject currentCreatedBuildable;
    [SerializeField] private RaycastHit mouseHitPoint;
    [SerializeField] private Color colorOnNormal;   

    private void Awake()
    {
        resources = FindObjectOfType<Resources>();

        for (int i=0; i<=35; i++)
        {
            string s = "Grid Element (" + i + ")";
            grid[i] = GameObject.Find(s).GetComponent<GridElement>();
        }

        colorOnNormal = grid[0].GetComponent<MeshRenderer>().material.color;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out mouseHitPoint))
        {
            GridElement g = mouseHitPoint.transform.gameObject.GetComponent<GridElement>();

            if (g == null)
            {
                if(currentHoveringGridElement)
                {
                    currentHoveringGridElement.GetComponent<MeshRenderer>().material.color = colorOnNormal;
                    currentHoveringGridElement = null;
                    return;
                }
            }

            if(Input.GetMouseButton(0))
            {
                currentSelectedGridElement = g;
                PlaceBuilding();
            }

            if (g != currentHoveringGridElement)
            {
                if (currentHoveringGridElement)
                {
                    currentHoveringGridElement.GetComponent<MeshRenderer>().material.color = colorOnNormal;
                }

                currentHoveringGridElement = g;
                if (g.occupied == false)
                {
                    currentHoveringGridElement.GetComponent<MeshRenderer>().material.color = colorOnHover;
                }
                else
                {
                    currentHoveringGridElement.GetComponent<MeshRenderer>().material.color = colorOnOccupied;
                }
            }
        }

        MoveBuilding();
    }

    public void OnButtonCreateBuilding(int id)
    {
        if(buildInProgress)
        {
            return;
        }

        GameObject g = null;
        foreach(GameObject gO in buildings.buildableObjects)
        {
            if(gO.GetComponent<Building>().buildingInformation.buildingID == id)
            {
                g = gO;
                break;
            }
        }
        currentCreatedBuildable = Instantiate(g);
        currentCreatedBuildable.transform.rotation = Quaternion.Euler(0f, transform.rotation.y - 225f, 0f);

        buildInProgress = true;        
    }

    public void MoveBuilding()
    {
        if(!currentCreatedBuildable)
        {
            return;
        }

        currentCreatedBuildable.gameObject.layer = 2;

        if (currentHoveringGridElement)
        {
            currentCreatedBuildable.transform.position = currentHoveringGridElement.transform.position;
        }

        if(Input.GetMouseButtonDown(1))
        {
            Destroy(currentCreatedBuildable);
            currentCreatedBuildable = null;
            buildInProgress = false;
        }   

        if(Input.GetMouseButton(2))
        {
            currentCreatedBuildable.transform.Rotate(transform.up * 5);
        }
    }

    public void PlaceBuilding()
    {
        if(!currentCreatedBuildable || currentHoveringGridElement.occupied)
        {
            return;
        }

        buildings.builtObjects.Add(currentCreatedBuildable);
        currentHoveringGridElement.occupied = true;

        Building building = currentCreatedBuildable.GetComponent<Building>();
        building.placed = true;
        building.UpgradeBuilding();
        currentHoveringGridElement.connectedBuilding = building;
        building.buildingInformation.connectedGridID = currentHoveringGridElement.gridID;
        building.buildingInformation.yRotation = currentCreatedBuildable.transform.localEulerAngles.y;
        
        currentCreatedBuildable = null;
        buildInProgress = false;
    }
}
