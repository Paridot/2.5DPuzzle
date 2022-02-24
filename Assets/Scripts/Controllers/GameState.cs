using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public delegate void TransitionEvent(float time, Structs.State view);
    public event TransitionEvent OnTransitionStart;


    [SerializeField] Structs.State state;

    [SerializeField] float transitionTime;

    float transitionTimeSpent;
    bool isTransitioning;

    void Awake()
    {
        transitionTimeSpent = transitionTime;
        isTransitioning = false;

        OnTransitionStart += GameState_OnTransitionStart;
    }

    private void GameState_OnTransitionStart(float time, Structs.State view)
    {
        //inverse state
        state = (Structs.State)(1 - (int)state);

        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        Time.timeScale = 0;
        transitionTimeSpent = 0;
        isTransitioning = true;

        while (transitionTimeSpent <= transitionTime)
        {
            transitionTimeSpent += Time.unscaledDeltaTime;
            yield return null;
        }
        isTransitioning = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (!isTransitioning && TransitionInitiated())
        {
            OnTransitionStart?.Invoke(transitionTime, (Structs.State)(1 - (int)state));
        }
    }


    bool TransitionInitiated()
    {
        if (Input.GetButtonDown("Transition"))
        {
            return true;
        }
        return false;
    }
}