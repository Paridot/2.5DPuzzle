using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class PlayerMouseInput : MonoBehaviour, IMover
{
    public event IMover.MoveInputEvent OnMoveInput;
    public event Action OnStop;

    [SerializeField] Camera cam;

    GameState gameState;

    private void Start() {
        gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();
    }

    private void Update() {
        if (!gameState.isTransitioning)
        {
            FaceMouse();

            if (Input.GetMouseButton(0))
            {
                OnMoveInput?.Invoke(transform.forward);
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnStop?.Invoke();
            }
        }
    }

    void FaceMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 playerPos = cam.WorldToScreenPoint(transform.position);

        if (gameState.state == State.Top)
        {
            mousePos.z = mousePos.y;
            playerPos.z = playerPos.y;
            mousePos.y = 0;
            playerPos.y = 0;
        } else 
        {
            mousePos.z = mousePos.x;
            playerPos.z = playerPos.x;
            mousePos.x = 0;
            playerPos.x = 0;
        }

        Vector3 relPos = playerPos - mousePos;

        transform.rotation = Quaternion.LookRotation(-relPos, Vector3.up);    
    }
}
