using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void Enter();
    void Update();
    void InputHandle();
    void Exit();
}
