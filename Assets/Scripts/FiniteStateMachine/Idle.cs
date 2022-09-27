using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{
    FiniteStateMachine _fsm;
    private Agent _agent;
    public Idle(FiniteStateMachine fsm, Agent a)
    {
        _agent = a;
        _fsm = fsm;
    }

    public void OnStart()
    {
        Debug.Log("Entre a Idle");
    }

    public void OnUpdate()
    {
        if (_agent.energy <= 0)
        {
            Debug.Log("Estoy en Idle");
        }
        else
        {
            _fsm.ChangeState(AgentStates.Patrol);
        }
    }

    public void OnExit()
    {

    }
}
