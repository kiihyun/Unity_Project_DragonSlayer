using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> 
{
    protected float elapsedTime;

    public State() { }
    public virtual void Init(T owner) { }
    public virtual void OnEnter()
    {
        elapsedTime = 0f;
         Debug.Log("State Entered: " + this.GetType().Name);
    }

    public virtual void OnUpdate(float deltaTime)
    {
        elapsedTime += deltaTime;
    }

    public virtual void OnFixedUpdate() { }

    public virtual void OnExit() { }







}
