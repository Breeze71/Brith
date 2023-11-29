using UnityEngine;
using V._Core;

namespace V
{
    public class ChaseInteraction : InteractableBase
    {
        private EntityBase entityBase;

        [SerializeField] private LayerMask elementMask;
        [SerializeField] private LayerMask enemyMask;
        
        private void Awake() 
        {
            entityBase = GetComponentInParent<EntityBase>();    
        }

        public override void EnterTrigger(Collider2D _other)
        {   
            // 是我方，判別是否點科技能否追逐或採集
            entityBase.TryGetComponent(out EntityCell _entityCell);
            if(_entityCell)
            {
                if(_entityCell.CanChaseElement() && (elementMask.value & (1 << _other.gameObject.layer)) > 0)
                {
                    entityBase.TargetTransform = _other.GetComponent<IDetectable>().GetTransform();
                    entityBase.SetAggroStatus(true);
                }
                
                else if(_entityCell.CanChaseEnemy() && (enemyMask.value & (1 << _other.gameObject.layer)) > 0)
                {
                    entityBase.TargetTransform = _other.GetComponent<IDetectable>().GetTransform();
                    entityBase.SetAggroStatus(true);
                }
            }

            // 是敵人
            else
            {
                entityBase.TargetTransform = _other.GetComponent<IDetectable>().GetTransform();
                entityBase.SetAggroStatus(true);
            }
        }

        public override void ExitTrigger(Collider2D _other)
        {
            entityBase.SetAggroStatus(false);
        }

        public override void Interact()
        {
            
        }



    }
}
