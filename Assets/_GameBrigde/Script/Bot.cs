using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    private Vector3 destination;
    public bool IsDestintion => Vector3.Distance(destination, transform.position.x * Vector3.right +
    Vector3.up * 0f + Vector3.forward * transform.position.z) < 0.1f;
    void Start()
    {
        destination = transform.position;
    }
    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destination = position;
        destination.y = 0f;
        agent.SetDestination(position);
    }
    IState<Bot> currentState;
    private void Update()
    {
        if (currentState != null)   
        {
            currentState.OnExecute(this);
            CanMove(transform.position);
        }
    }
    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
