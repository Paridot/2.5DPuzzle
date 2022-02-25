using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IMover
{
    public delegate void MoveInputEvent(Vector3 direction);
    public event MoveInputEvent OnMoveInput; 

    public event Action OnStop; 
    
}
