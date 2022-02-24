using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    struct Directions
    {
        public Vector3 Up;
        public Vector3 Down;
        public Vector3 Left;
        public Vector3 Right;
    }

    Directions directions;

    public delegate void MoveInputEvent(Vector3 direction);
    public event MoveInputEvent OnMoveInput; 

    [SerializeField] Camera cam;

    [SerializeField] int speed;

    Structs.State view;
    bool isTransitioning = false;
    private float transitionTime;
    private float timeSpent;

    private void Start() {
        GameState gameState = GameObject.Find("StateController").GetComponent<GameState>();

        if (gameState != null)
        {
            gameState.OnTransitionStart += GameState_OnTransitionStart;

            SetDirections(Structs.State.Top);
        }
    }

    void Update()
    {
        if (!isTransitioning)
        {
            FaceMouse();

            if (Input.GetMouseButton(0))
            {
                transform.position += transform.forward * Time.deltaTime * speed;
            }

            CheckMoveInput();

        }
    }

    private void CheckMoveInput()
    {
        Vector3 directionToSend = Vector3.zero;

        if (Input.GetButton("Up")) directionToSend += directions.Up;
        if (Input.GetButton("Down")) directionToSend += directions.Down;
        if (Input.GetButton("Left")) directionToSend += directions.Left;
        if (Input.GetButton("Right")) directionToSend += directions.Right;

        if (directionToSend != Vector3.zero)
        {
            OnMoveInput?.Invoke(directionToSend.normalized);
        }
    }

    void SetDirections(Structs.State s)
    {
        if (s == Structs.State.Top)
        {
            directions.Up = Vector3.forward;
            directions.Down = Vector3.back;
            directions.Left = Vector3.left;
            directions.Right = Vector3.right;
        } else {
            directions.Up = Vector3.up;
            directions.Down = Vector3.down;
            directions.Left = Vector3.back;
            directions.Right = Vector3.forward;
        }
    }

    private void GameState_OnTransitionStart(float time, Structs.State v)
    {
        view = v;
        transitionTime = time;
        SetDirections(v);
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        isTransitioning = true;
        timeSpent = 0;
        while(timeSpent <= transitionTime)
        {
            timeSpent += Time.unscaledDeltaTime;
            yield return null;
        }
        isTransitioning = false;
    }

    void FaceMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 playerPos = cam.WorldToScreenPoint(transform.position);

        if (view == Structs.State.Top)
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
