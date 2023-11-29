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
        public Element element;   // 屬性
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

            // 判別是哪屬性
            if(element == Element.Ground)
            {
                _entityBase.EntityElement.GroundElement++;
            }
            else if(element == Element.Fire)
            {
                _entityBase.EntityElement.FireElement++;
            }
            else if(element == Element.Wind)
            {
                _entityBase.EntityElement.WindElement++;
            }
            else if(element == Element.Water)
            {
                _entityBase.EntityElement.WaterElement++;
            }

            _entityBase.ElementChangeEvent();

            // To - Do 
        }

        public override void ExitTrigger(Collider2D _other)
        {
            
        }

        public override void Interact()
        {
            
        }
    }
}
