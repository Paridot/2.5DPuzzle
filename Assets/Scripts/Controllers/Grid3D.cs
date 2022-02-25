using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3D : MonoBehaviour
{
    public Vector3 gridSize;

    private List<Vector3Int> points;


    //Poor performance O(n^3) where n is 1 dimension of the cube. 
    //Each dimension is only meant to be between 10 and 40 units large 
    //so this shouldn't impact performance when it is called infrequently 
    //(5000-10000 points in list).

    //Should probably reimplement this if this becomes used more frequently
    public Vector3Int GetNearestPoint(Vector3 position)
    {
        
        var distance = Vector3.Distance(points[0], position);
        Vector3Int closestPoint = points[0];
        foreach (var point in points)
        {

            var testDist = Vector3.Distance(CellToWorldPoint(point), position);
            if (distance > testDist)
            {
                distance = testDist;
                closestPoint = point;
            }
        }
        return closestPoint;
    }

    public Vector3 CellToWorldPoint(Vector3Int point)
    {
        var mapPos = transform.position;
        var worldPoint = point + mapPos; 
        return worldPoint;
    }

    private void Start() {
        points = new List<Vector3Int>();
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    points.Add(new Vector3Int(x,y,z));
                }
            }
        }
    }
}
