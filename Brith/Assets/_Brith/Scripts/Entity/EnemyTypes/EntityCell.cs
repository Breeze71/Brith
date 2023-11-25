using System;
using UnityEngine;

namespace V
{
    public class EntityCell : EntityBase, IDamagable
    {   
        #region Combat Var
        public int defense;
        public int speed;
        [field : SerializeField] public int maxHealth {get; set ;}
        #endregion

        #region IDamagable
        [field : SerializeField] public HealthBarUI HealthBarUI { get; set; }
        public HealthSystem HealthSystem { get; set;}
        public int HealthAmount { get; set; }
        #endregion

        #region Element && Reproduce
        [SerializeField] private int elementAmountToReproduce = 200;
        [SerializeField] private GameObject element;
        public EntityElement entityElement{ get; set;}   // 存儲元素 
        private event Action OnElementChange; // Element Change
        #endregion

        protected override void Start()
        {
            base.Start();

            HealthSystem = new HealthSystem(maxHealth);
            HealthBarUI.SetupHealthSystemUI(HealthSystem);

            entityElement = new EntityElement();

            OnElementChange += BasicEntity_ElementChange;
        }

        #region Health
        public void TakeDamage(int _attack)
        {
            int _countDamage = _attack - defense;

            HealthSystem.TakeDamage(_countDamage);

            if(HealthSystem.GetHealthAmount() <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            // TO - DO 每有五十点元素结晶就变成一个随机方向的新元素粒子。
            int totalElement = entityElement.GetTotalElementAmount();

            while(totalElement >= 50)
            {
                Instantiate(element);

                totalElement -= 50;
            }

            Destroy(gameObject);
        }
        #endregion
    
        #region Reproduce
        /// <summary>
        /// invoke when Collect Element
        /// </summary>
        public void ElementChangeEvent()
        {
            OnElementChange?.Invoke();
        }

        private void BasicEntity_ElementChange()
        {
            // 檢查元素總量
            if(entityElement.GetTotalElementAmount() >= elementAmountToReproduce)
            {
                entityElement.DecreaseElement();

                // 生出新 Entity
                Instantiate(gameObject);
            }
        }

        #endregion
    
        #region Test
        [ContextMenu("TestDead()")]
        private void TestDead()
        {
            TakeDamage(110);
        }
        [ContextMenu("TestCollect10Element()")]
        private void TestCollect100Element()
        {
            entityElement.FireElement += 100;
            ElementChangeEvent();
        }
        #endregion
    }
}
