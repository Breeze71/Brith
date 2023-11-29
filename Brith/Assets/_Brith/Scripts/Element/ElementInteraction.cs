using System;
using UnityEngine;
using V._Core;

namespace V
{   
    /// <summary>
    /// 地 火 風 水
    /// </summary>
    public enum Element
    {
        Ground,
        Fire,
        Wind,
        Water,
    }

    public class ElementInteraction : InteractableBase
    {
        public Element elementType;   // 屬性
        public InjectElement InjectElement;

        private bool isCollected;

        private void OnEnable() 
        {
            isCollected = false;  
        }

        public override void EnterTrigger(Collider2D _other)
        {
            EntityBase _entityBase = _other.GetComponent<EntityBase>();

            if(_entityBase == null)
            {
                return;
            }

            if(isCollected)
            {
                return;
            }

            gameObject.SetActive(false);
            isCollected = true;
            InjectElement.currentElementAmount--;

            _entityBase.GetElement(elementType);

            _entityBase.ElementChangeEvent();
        }

        public override void ExitTrigger(Collider2D _other)
        {
            
        }

        public override void Interact()
        {
            
        }
    }
}
