public interface IState
{
    public void Enter();
    public void Update();
    public void Exit();
}
public class E_StateMachine
{
    protected IState currentState;
    public IState CurrentState => currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();   //nullable로 만들어서 null 체크
        currentState = newState;
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
