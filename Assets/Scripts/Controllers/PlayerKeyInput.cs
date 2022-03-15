using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyInput : MonoBehaviour, IMover
{

    GameState gameState;

    public event IMover.MoveInputEvent OnMoveInput;
    public event Action OnStop;

    private void Start() {
        gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();
    }

    private void Update() {
        CheckMoveInput();
    }

    private void CheckMoveInput()
    {
        Vector3 directionToSend = Vector3.zero;

        if (Input.GetButton("Up")) directionToSend += gameState.directions.Up;
        if (Input.GetButton("Down")) directionToSend += gameState.directions.Down;
        if (Input.GetButton("Left")) directionToSend += gameState.directions.Left;
        if (Input.GetButton("Right")) directionToSend += gameState.directions.Right;

        if (directionToSend != Vector3.zero)
        {
            OnMoveInput?.Invoke(directionToSend.normalized);
        }

        if (Input.GetButtonUp("Up") || Input.GetButtonUp("Down") || Input.GetButtonUp("Left") || Input.GetButtonUp("Right"))
        {
            OnStop?.Invoke();
        }
    }
}
