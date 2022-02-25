using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    private Grid3D grid;
    [SerializeField] private GameObject sheet;
    [SerializeField] private GameObject sheet1;

    void Start()
    {
        grid = GetComponent<Grid3D>();
        transform.localScale = grid.gridSize;

        for (int x = 0; x <= grid.gridSize.x; x++)
        {
            Quaternion rotation = Quaternion.Euler(0, -90, 0);
            var newSheet = Instantiate(sheet,new Vector3(x-0.5f, grid.gridSize.y/2-0.5f, grid.gridSize.z/2-0.5f), rotation, transform);
        }
        for (int y = 0; y <= grid.gridSize.x; y++)
        {
            Quaternion rotation = Quaternion.Euler(90, 0, 0);
            var newSheet = Instantiate(sheet1,new Vector3(grid.gridSize.x/2-0.5f, y-0.5f, grid.gridSize.z/2-0.5f), rotation, transform);
        }
    }
}
