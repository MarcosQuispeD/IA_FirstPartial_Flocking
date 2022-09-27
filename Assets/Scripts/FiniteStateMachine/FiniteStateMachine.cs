using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    private IState _currentState;
    private Dictionary<AgentStates, IState> _allStates = new Dictionary<AgentStates, IState>();

    public void Update()
    {
        _currentState.OnUpdate();
    }

    public void ChangeState(AgentStates state)
    {
        if (!_allStates.ContainsKey(state)) return;

        if (_currentState != null) _currentState.OnExit();
        _currentState = _allStates[state];
        _currentState.OnStart();
    }
    public void AddState(AgentStates key, IState value)
    {
        if (!_allStates.ContainsKey(key)) _allStates.Add(key, value);
        else _allStates[key] = value;
    }
}
