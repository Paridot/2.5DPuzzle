using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Types
{
    public enum State
    {
        Top,
        Side
    }

    [System.Serializable]
    public class MapObject
    {
        public Vector3Int point;
        public GameObject obj;

        public MapObject(Vector3Int p, GameObject o)
        {
            point = p;
            obj = o;
        }
    }
}
