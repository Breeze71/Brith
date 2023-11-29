using UnityEngine;
using V._Core;

namespace V
{
    public class ChaseInteraction : InteractableBase
    {
        private EntityBase enemyBase;

        [SerializeField] private EnemyChaseSOBase runOutofSO;
        [SerializeField] private EnemyChaseSOBase enemyChaseSO;
        
        private void Awake() 
        {
            enemyBase = GetComponentInParent<EntityBase>();    
        }

        public override void EnterTrigger(Collider2D _other)
        {
            // if(enemyBase.CurrentEntityState == EntityState.Collecting && enemyBase.CurrentEntityState == EntityState.Chasing)
            // {
            //     enemyBase.TargetTransform = _other.gameObject.transform;
            //     // enemyBase.ChaseState = new EnemyChaseSOBase(); // 將行為變成逃離
            //     enemyBase.SetAggroStatus(true); // 轉至下個狀態
            // }
            // else if(enemyBase.CurrentEntityState == EntityState.Running)
            // {
            //     enemyBase.TargetTransform = _other.gameObject.transform;
            //     // enemyBase.ChaseState = new EnemyChaseSOBase(); // 將行為變成逃離
            //     enemyBase.SetAggroStatus(true); // 
            // }

            enemyBase.TargetTransform = _other.GetComponent<IDetectable>().GetTransform();
            enemyBase.SetAggroStatus(true);
        }

        public override void ExitTrigger(Collider2D _other)
        {
            enemyBase.SetAggroStatus(false);
        }

        public override void Interact()
        {
            
        }



    }
}
