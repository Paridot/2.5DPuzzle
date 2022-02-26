using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    private Grid3D grid;
    [SerializeField] private GameObject sheet;
    [SerializeField] private GameObject gridLine;

    GameState gameState;

    private float thinScale = 0.03f;

    void Start()
    {
        gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();

        if (gameState != null)
        {
            gameState.OnTransitionStart += GameState_OnTransitionStart;
        }

        grid = GetComponent<Grid3D>();
        transform.localScale = grid.gridSize;

        var offset = transform.position - new Vector3(0.5f, 0.5f, 0.5f);

        for (int x = 0; x <= grid.gridSize.x; x++)
        {
            Quaternion rotation = Quaternion.Euler(0, -90, 0);
            var newSheet = Instantiate(sheet, new Vector3(x, grid.gridSize.y / 2, grid.gridSize.z / 2) + offset, rotation, transform);
        }
        for (int y = 0; y <= grid.gridSize.x; y++)
        {
            Quaternion rotation = Quaternion.Euler(90, 0, 0);
            var newSheet = Instantiate(sheet, new Vector3(grid.gridSize.x / 2, y, grid.gridSize.z / 2) + offset, rotation, transform);

            // var newGridLine = Instantiate(gridLine,new Vector3(grid.gridSize.x/2, y, grid.gridSize.z/2)+offset, rotation, transform);

            // newGridLine.transform.localScale = new Vector3(newGridLine.transform.localScale.x, thinScale *(1/grid.gridSize.y),thinScale * (1/grid.gridSize.z));
        }

        for (int y = 0; y <= grid.gridSize.y; y++)
        {
            for (int z = 0; z <= grid.gridSize.z; z++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 0);
                var newGridLine = Instantiate(gridLine, new Vector3(grid.gridSize.x/2, y, z) + offset, rotation, transform);

                newGridLine.transform.localScale = new Vector3(newGridLine.transform.localScale.x, thinScale * (1 / grid.gridSize.y), thinScale * (1 / grid.gridSize.z));
            }
        }
        for (int x = 0; x <= grid.gridSize.y; x++)
        {
            for (int z = 0; z <= grid.gridSize.z; z++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 90);
                var newGridLine = Instantiate(gridLine, new Vector3(x, grid.gridSize.y/2, z) + offset, rotation, transform);

                newGridLine.transform.localScale = new Vector3(newGridLine.transform.localScale.x, thinScale * (1 / grid.gridSize.y), thinScale * (1 / grid.gridSize.z));
            }
        }
        for (int x = 0; x <= grid.gridSize.y; x++)
        {
            for (int y = 0; y <= grid.gridSize.z; y++)
            {
                Quaternion rotation = Quaternion.Euler(0, 90, 0);
                var newGridLine = Instantiate(gridLine, new Vector3(x, y, grid.gridSize.z/2) + offset, rotation, transform);

                newGridLine.transform.localScale = new Vector3(newGridLine.transform.localScale.x, thinScale * (1 / grid.gridSize.y), thinScale * (1 / grid.gridSize.z));
            }
        }
    }

    private void GameState_OnTransitionStart(float time, Structs.State view)
    {

    }
}
