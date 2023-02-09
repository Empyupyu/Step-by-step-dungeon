using System.Collections.Generic;
using System;
using UnityEngine;

public sealed class StateMachine<T> where T :Enum
{
    public event Action<T> OnStateChange;

    private IState<T> _currentState;
    private readonly Dictionary<T, IState<T>> _states = new Dictionary<T, IState<T>>();

    public void AddState(IState<T> state)
    {
        _states.Add(state.StateType, state);
    }

    public void SetState(T stateType)
    {
        var newUnitState = _states[stateType];

        if (newUnitState == _currentState)
        {
            Debug.LogWarning($"Same state set warning: {_currentState}");
            return;
        }

        _currentState?.OnExit();

        _currentState = newUnitState;
        _currentState.OnEnter();

        OnStateChange?.Invoke(_currentState.StateType);
    }

    public void Tick()
    {
        _currentState.Tick();
    }
}