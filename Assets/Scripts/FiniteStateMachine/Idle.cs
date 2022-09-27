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
        Debug.Log("Estoy en Idle");
        _agent.energy = Mathf.Clamp(_agent.energy += Time.deltaTime, 0, _agent.energyMax);
        if (_agent.energy == _agent.energyMax) _fsm.ChangeState(AgentStates.Patrol);
    }

    public void OnExit()
    {

    }
}
