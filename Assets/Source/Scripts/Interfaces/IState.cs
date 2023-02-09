using System;

public abstract class IState<T> where T : Enum
{
    public T StateType { get; }

    protected readonly StateMachine<T> stateMachine;

    protected IState(StateMachine<T> stateMachine, T type)
    {
        this.stateMachine = stateMachine;
        StateType = type;
    }

    public abstract void Tick();
    public abstract void OnEnter();
    public abstract void OnExit();
}