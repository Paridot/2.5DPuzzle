using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    private Grid grid;
    [SerializeField] private GameObject sheet;
    [SerializeField] private Vector3 gridSize;

    void Start()
    {
        grid = GetComponent<Grid>();
        transform.localScale = gridSize;

        for (int x = 0; x <= gridSize.x; x++)
        {
            Quaternion rotation = Quaternion.Euler(0, -90, 0);
            var newSheet = Instantiate(sheet,new Vector3(x, gridSize.y/2, gridSize.z/2), rotation, transform);
        }
        for (int y = 0; y <= gridSize.x; y++)
        {
            Quaternion rotation = Quaternion.Euler(90, 0, 0);
            var newSheet = Instantiate(sheet,new Vector3(gridSize.x/2, y, gridSize.z/2), rotation, transform);
        }
    }
    void Update()
    {
        
    }
}
