using System.Collections;
using System.Collections.Generic;
using Types;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public delegate void TransitionEvent(float time, State view);
    public event TransitionEvent OnTransitionStart;

    public struct Directions
    {
        public Vector3 Up;
        public Vector3 Down;
        public Vector3 Left;
        public Vector3 Right;
        public Directions(Vector3 up, Vector3 down, Vector3 left, Vector3 right)
        {
            this.Up = up;
            this.Down = down;
            this.Left = left;
            this.Right = right;
        }
    }

    public Directions directions {get; private set;}

    public State state {get; private set;}
    public bool isTransitioning {get; private set;} 

    [SerializeField] float transitionTime;

    float transitionTimeSpent;

    void Awake()
    {
        transitionTimeSpent = transitionTime;
        isTransitioning = false;

        OnTransitionStart += GameState_OnTransitionStart;

        SetDirections(state);
    }

    private void GameState_OnTransitionStart(float time, State view)
    {
        //inverse state
        state = (State)(1 - (int)state);

        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        Time.timeScale = 0;
        transitionTimeSpent = 0;
        isTransitioning = true;

        SetDirections(state);

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
            OnTransitionStart?.Invoke(transitionTime, (State)(1 - (int)state));
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

    void SetDirections(State s)
    {
        if (s == State.Top)
        {
            directions = new Directions(
                Vector3.forward,
                Vector3.back,
                Vector3.left,
                Vector3.right
            );
        } else {
            directions = new Directions(
                Vector3.up,
                Vector3.down,
                Vector3.back,
                Vector3.forward
            );
        }
    }
}