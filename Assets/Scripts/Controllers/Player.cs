using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] Camera cam;

    private float transitionTime;
    private float timeSpent;

    GameState gameState;

    private void Start() {
        gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();

        if (gameState != null)
        {
            gameState.OnTransitionStart += GameState_OnTransitionStart;
        }
    }


    private void GameState_OnTransitionStart(float time, Structs.State v)
    {
        transitionTime = time;
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        timeSpent = 0;
        while(timeSpent <= transitionTime)
        {
            timeSpent += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    
}
