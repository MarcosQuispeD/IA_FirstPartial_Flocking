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
    public float speed = 5;
    public float energy;
    public float timeResetEnergy;

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
        if (energy > 0) energy -= Time.deltaTime;
        //else energy += Time.deltaTime;

        //if (energy >= timeResetEnergy) energy = timeResetEnergy;
        _FSM.Update();
    }

    public void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}
