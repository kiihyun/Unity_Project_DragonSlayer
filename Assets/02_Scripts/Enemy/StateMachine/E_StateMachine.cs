public interface IState
{
    public void Enter();
    public void Update();
    public void Exit();
}
public class E_StateMachine
{
    protected IState _currentState;
    public IState CurrentState => _currentState;

    public void ChangeState(IState newState)
    {
        _currentState?.Exit();   //nullable로 만들어서 null 체크
        _currentState = newState;
        _currentState?.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }
}
