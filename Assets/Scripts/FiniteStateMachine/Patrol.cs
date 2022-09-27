using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : IState
{
    private GameObject[] _waypoints;
    private int _currentWP = 0; //esto lo podria tener en el agente
    private Agent _agent;
    private FiniteStateMachine _fsm;

    public Patrol(FiniteStateMachine fsm, Agent a)
    {
        _fsm = fsm;
        _agent = a;
    }

    public void OnStart()
    {
        Debug.Log("Entre a patrol");
        _waypoints = _agent.allWaypoints;

        _agent.ChangeColor(Color.green);
    }

    public void OnUpdate()
    {
        if (_agent.energy <= 0)
        {
            _fsm.ChangeState(AgentStates.Idle);
        }

        PatrolState();
    }

    void PatrolState()
    {
        GameObject waypoint = _waypoints[_currentWP];
        Vector3 dir = waypoint.transform.position - _agent.transform.position;
        _agent.transform.forward = dir;
        _agent.transform.position += _agent.transform.forward * _agent.speed * Time.deltaTime;
        if (dir.magnitude <= 0.2f)
        {
            _currentWP++;
            if (_currentWP > _waypoints.Length - 1)
            {
                _currentWP = 0;
            }
        }
    }

    public void OnExit()
    {
        _agent.ChangeColor(Color.gray);
    }
}
