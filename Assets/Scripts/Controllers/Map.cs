using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    private Grid3D grid;
    [SerializeField] private GameObject sheet;

    GameState gameState;

    void Start()
    {
        gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();

        if (gameState != null)
        {
            gameState.OnTransitionStart += GameState_OnTransitionStart;
        }

        grid = GetComponent<Grid3D>();
        transform.localScale = grid.gridSize;

        for (int x = 0; x <= grid.gridSize.x; x++)
        {
            Quaternion rotation = Quaternion.Euler(0, -90, 0);
            var newSheet = Instantiate(sheet,new Vector3(x, grid.gridSize.y/2, grid.gridSize.z/2), rotation, transform);
        }
        for (int y = 0; y <= grid.gridSize.x; y++)
        {
            Quaternion rotation = Quaternion.Euler(90, 0, 0);
            var newSheet = Instantiate(sheet,new Vector3(grid.gridSize.x/2, y, grid.gridSize.z/2), rotation, transform);
        }
    }

    private void GameState_OnTransitionStart(float time, Structs.State view)
    {
        
    }
}
