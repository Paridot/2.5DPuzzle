using System;
using System.Collections;
using System.Collections.Generic;
using Types;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] GameObject subject;

    [SerializeField] int viewDistance;

    CamPosition startTransitionPos;
    CamPosition endTransitionPos;

    private float transitionTime;
    private float timeSpent;

    GameState gameState;

    Vector3 gridSize;


    private void Start()
    {
        gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();

        if (gameState != null)
        {
            gameState.OnTransitionStart += GameState_OnTransitionStart;
        }

        gridSize = GameObject.FindWithTag("Map").GetComponent<Grid3D>().gridSize;
    }
    
    void Update()
    {
        if (!gameState.isTransitioning)
        {
            UpdateView();
        } 
    }

    private void GameState_OnTransitionStart(float time, State v)
    {
        transitionTime = time;
        startTransitionPos = GetPosition((State)(1 - (int)v));
        endTransitionPos = GetPosition(v);
        
        StartCoroutine(Transition());
    }


    IEnumerator Transition()
    {

        timeSpent = 0;
        var prevLerpPoint = 0f;
        while (timeSpent <= transitionTime)
        {
            var lerpPoint = timeSpent/transitionTime;
            // transform.position = Vector3.Lerp(startTransitionPos.position, endTransitionPos.position, lerpPoint);
            transform.rotation = Quaternion.Lerp(startTransitionPos.rotation, endTransitionPos.rotation, lerpPoint);
            var angle = (90 / transitionTime);
            if (gameState.state == State.Side) angle *= -1;
            transform.RotateAround(subject.transform.position, Vector3.forward, angle * Time.unscaledDeltaTime);
            prevLerpPoint = lerpPoint;
            timeSpent += Time.unscaledDeltaTime;
            yield return null;
        }
    }


    CamPosition GetPosition(State v)
    {
        var subPos = subject.transform.position;
        CamPosition pos = new CamPosition();
        if (v == State.Top)
        {
            pos.rotation = Quaternion.Euler(90, 0, 0);
            pos.position = new Vector3(subPos.x, subPos.y + viewDistance, subPos.z);
        }
        else //Side View
        {
            pos.rotation = Quaternion.Euler(0, -90, 0);
            pos.position = new Vector3(subPos.x + viewDistance, subPos.y, subPos.z);
        }

        return pos;
    }

    void UpdateView()
    {
        transform.position = GetPosition(gameState.state).position;
        transform.rotation = GetPosition(gameState.state).rotation;
    }
}

public class CamPosition
{
    public Vector3 position;
    public Quaternion rotation;

    public CamPosition(Vector3 pos, Quaternion rot)
    {
        position = pos;
        rotation = rot;
    }

    public CamPosition()
    {

    }
}