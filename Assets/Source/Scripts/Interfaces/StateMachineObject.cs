using System;

public abstract class StateMachineObject<T> : Unit where T : Enum
{
    protected StateMachine<T> StateMachine { get; } = new StateMachine<T>();

    protected T currentState;

    protected virtual void Awake()
    {
        GetComponents();

        Init();

        LoadStates();

        StateMachine.OnStateChange += type => currentState = type;

        SetInitialState();
    }

    protected abstract void SetInitialState();
    protected abstract void GetComponents();
    protected abstract void Init();
    protected abstract void LoadStates();

    private void Update()
    {
        StateMachine.Tick();
    }
}