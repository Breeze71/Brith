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

        public EntityElement entityElement{ get; set;}   // 存儲元素 
        private event Action OnElementChange; // Element Change

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
            if(entityElement.GetTotalElementAmount() >= 50)
            {
                entityElement.DecreaseElement();

                // TO - Do 生出新 Entity
                Debug.LogError("Reproduce");
            }
        }

        #endregion
    }
}
