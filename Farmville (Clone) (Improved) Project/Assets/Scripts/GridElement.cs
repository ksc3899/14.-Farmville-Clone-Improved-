using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    public int gridID;
    public bool occupied;
    public Building connectedBuilding;
    //TODO: Which building is connected.

    private void Start()
    {
        Build b = FindObjectOfType<Build>();
        for (int i=0; i<b.grid.Length; i++)
        {
            if (b.grid[i].transform == transform)
            {
                gridID = i;
                break;
            }
        }
    }
}
