using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    struct MoveParams
    {
        public Vector3 start;
        public Vector3 end;

        public float timeToArrive;
        public float elapsed;
    };
    [SerializeField] private float speed;
    private bool isMoving;

    private MoveParams moveParams;

    private void Start() {
        var player = GetComponent<Player>();
    }

    private void Update() 
    {
        if (isMoving)
        {
            Move();
        }
    }

    public void InitiateMove(Vector3 dir)
    {
        if (isMoving) return;
        isMoving = true;
        
        moveParams.start = transform.position;
        moveParams.end = transform.position + dir;

        moveParams.timeToArrive = 1/speed;
        moveParams.elapsed = 0f;
    }

    private void Move()
    {
        var percentTransitioned = moveParams.elapsed/moveParams.timeToArrive;
        transform.position = Vector3.Lerp(moveParams.start,moveParams.end, percentTransitioned);

        if (percentTransitioned >= 1f) isMoving = false;
    }
}
