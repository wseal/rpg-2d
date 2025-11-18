
public class StateMachine
{
     public EntityState currentState { get; private set; }

     public void Initialize(EntityState state)
     {
          currentState = state;
          currentState.Enter();
     }

     public void ChangeState(EntityState newState)
     {
          currentState.Exit();
          currentState = newState;
          currentState.Enter();
     }

     public void UpdateActiveState()
     {
          currentState.Update();
     }
}
