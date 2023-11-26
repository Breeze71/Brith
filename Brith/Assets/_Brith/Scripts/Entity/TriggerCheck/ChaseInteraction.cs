using UnityEngine;
using V._Core;

namespace V
{
    public class ChaseInteraction : InteractableBase
    {
        private EntityBase enemyBase;
        
        private void Awake() 
        {
            enemyBase = GetComponentInParent<EntityBase>();    
        }

        public override void EnterTrigger(Collider2D _other)
        {
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
