using UnityEngine;



namespace V
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentEnemyState{get; set;}

        public void Initalize(EnemyState _startState)
        {
            CurrentEnemyState = _startState;
            CurrentEnemyState.EnterState();
        }
        public void ChangeState(EnemyState _newState)
        {
            CurrentEnemyState.ExitState();
            CurrentEnemyState = _newState;
            CurrentEnemyState.EnterState();
        }
    }
}
