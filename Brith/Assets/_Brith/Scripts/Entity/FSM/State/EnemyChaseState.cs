
namespace V
{
    public class EnemyChaseState : EnemyState
    {
        public EnemyChaseState(EntityBase _enemyBase, EnemyStateMachine _enemyStateMachine) : base(_enemyBase, _enemyStateMachine)
        {
        }

        public override void EnterState() 
        {
            base.EnterState();

            enemyBase.EnemyChaseBaseInstance.DoEnterState();
        }
        
        public override void FrameUpdate() 
        {
            base.FrameUpdate();

            enemyBase.EnemyChaseBaseInstance.DoFrameUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            enemyBase.EnemyChaseBaseInstance.DoPhysicsUpdate();
        }

        public override void ExitState()
        {
            base.ExitState();

            enemyBase.EnemyChaseBaseInstance.DoExitState();
        }

        public override void AnimTriggerEvent(EntityBase.AnimTriggerTypes _triggerTypes)
        {
            base.AnimTriggerEvent(_triggerTypes);

            enemyBase.EnemyChaseBaseInstance.DoAnimTriggerEvent(_triggerTypes);
        }
    }
}
