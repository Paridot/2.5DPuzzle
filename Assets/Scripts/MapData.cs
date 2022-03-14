using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Types;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Level Design/Map", order = 0)]
public class MapData : ScriptableObject {
    public Vector3Int size;
    public Vector3Int playerStart;
    public List<MapObject> mapObjects;

    public void GenerateObjects()
    {
        mapObjects = new List<MapObject>();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int z = 0; z < size.z; z++)
                {
                    mapObjects.Add(new MapObject(new Vector3Int(x,y,z), null));
                }
            }
        }
    }

    public MapObject GetMapObject(Vector3Int point)
    {
        return mapObjects.Find(i => i.point == point);
    }

}