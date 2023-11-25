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

        [SerializeField] private GameObject visual;
        private bool isCollected = false;

        public override void EnterTrigger(Collider2D _other)
        {
            EntityCell _basicEntity = _other.GetComponent<EntityCell>();

            if(_basicEntity == null)
            {
                return;
            }

            if(isCollected)
            {
                return;
            }

            visual.SetActive(false);
            isCollected = true;

            // 判別是哪屬性
            if(element == Element.Ground)
            {
                _basicEntity.entityElement.GroundElement++;
            }
            else if(element == Element.Fire)
            {
                _basicEntity.entityElement.FireElement++;
            }
            else if(element == Element.Wind)
            {
                _basicEntity.entityElement.WindElement++;
            }
            else if(element == Element.Water)
            {
                _basicEntity.entityElement.WaterElement++;
            }

            _basicEntity.ElementChangeEvent();
        }

        public override void ExitTrigger(Collider2D _other)
        {
            
        }

        public override void Interact()
        {
            
        }
    }
}
