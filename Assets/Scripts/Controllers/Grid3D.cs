using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3D : MonoBehaviour
{
    [SerializeField] public Vector3 gridSize;

    private List<Vector3Int> points;

    public Vector3 GetNearestPoint(Vector3 position)
    {
        var distance = Vector3.Distance(points[0], position);
        Vector3 closestPoint = points[0];
        foreach (var point in points)
        {
            var testDist = Vector3.Distance(point, position);
            if (distance > testDist)
            {
                distance = testDist;
                closestPoint = point;
            }
        }
        return closestPoint;
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
