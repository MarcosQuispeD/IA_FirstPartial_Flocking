using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AgentStates
{
    Idle,
    Patrol
}


public class Agent : MonoBehaviour
{
    private FiniteStateMachine _FSM;
    public GameObject[] allWaypoints;
    int _currentWP = 0;
    public float speed = 5;
    public float energy;
    public float energyMax;

    public int CurrentWp
    {
        get { return _currentWP; }
        set { _currentWP = value; }
    }

    private void Start()
    {
        _FSM = new FiniteStateMachine();

        _FSM.AddState(AgentStates.Idle, new Idle(_FSM, this));
        _FSM.AddState(AgentStates.Patrol, new Patrol(_FSM, this));
        _FSM.ChangeState(AgentStates.Idle);

        ChangeColor(Color.gray);
    }

    private void Update()
    {
        _FSM.Update();
    }

    public void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}
