using UnityEngine;
using V._Core;

namespace V
{
    public class AttackInteraction : InteractableBase
    {
        private EntityBase enemyBase;

        private void Awake() 
        {
            enemyBase = GetComponentInParent<EntityBase>();    
        }

        public override void EnterTrigger(Collider2D _other)
        {
            enemyBase.SetAttackStatus(true);

            _other.GetComponentInChildren<IDamagable>().TakeDamage(enemyBase.attack);
        }

        public override void ExitTrigger(Collider2D _other)
        {
            enemyBase.SetAttackStatus(false);
        }

        public override void Interact()
        {
            
        }

    }
}
