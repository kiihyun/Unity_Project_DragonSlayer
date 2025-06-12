public class BossStateMachine
{
    private IBossState _currentState;

    public void Initialize(IBossState startState)
    {
        _currentState = startState;
        _currentState.Enter();
    }

    public void ChangeState(IBossState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Execute();
    }
}