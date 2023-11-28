using System;
using UnityEngine;

namespace V
{
    public class EntityCell : EntityBase, IDamagable
    {   
        #region Combat Var
        [field : SerializeField] public int defense {get; private set;}
        [field : SerializeField] public int maxHealth {get; private set ;}
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
        public event Action OnReproduce;
        #endregion

        #region Tech
        [Header("Tech")]
        public float sp_1 = 5;
        public float sp_2 = 10;
        public int hp_1 = 50;
        public int hp_2 = 80;
        #endregion

        private CellTech cellTech;

        public override void InitalizeEntity()
        {
            base.InitalizeEntity();

            HealthSystem = new HealthSystem(maxHealth);
            entityElement = new EntityElement();

        }
        protected override void SetEntity()
        {
            HealthBarUI.SetupHealthSystemUI(HealthSystem);

            OnElementChange += BasicEntity_ElementChange;  // pick up element

            cellTech = GameObject.FindGameObjectWithTag("CellTag").GetComponent<CellTech>();           
            cellTech.OnUnlockedNewTech += CellTech_OnUnlockedNewTech; // add new tech
            cellTech.CheckUnlockSkill();
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
                OnReproduce?.Invoke();  // 改變 move 方向

                // TO - DO 生出新 Entity
                Instantiate(gameObject);
            }
        }

        #endregion
    
        #region Tech
        /// <summary>
        /// Recive Add Tech Event 
        /// </summary>
        private void CellTech_OnUnlockedNewTech(TechType techType)
        {
            // To - Do Lot of Tech
            switch(techType)
            {
                case TechType.MoveSpeed_1:
                    SetMinStableSpeed(sp_1);
                    break;
                case TechType.MoveSpeed_2:
                    SetMinStableSpeed(sp_2);
                    break;
                case TechType.HealthMax_1:
                    SetMaxHealthAmount(hp_1);
                    break;
                case TechType.HealthMax_2:
                    SetMaxHealthAmount(hp_2);
                    break;                
            }
        }

        private void CheckUnlockTech()
        {
            cellTech.CheckUnlockSkill();
        }

        public CellTech GetCellTech()
        {
            return cellTech;
        }
        public bool CanUseFollow()
        {
            return cellTech.IsTechUnlocked(TechType.Follow);
        }

        private void SetMinStableSpeed(float _speed)
        {
            speed += _speed;

            Debug.Log(speed);
        }
        private void SetMaxHealthAmount(int _amount)
        {
            HealthSystem.ChangeMaxHealth(maxHealth + _amount);

            Debug.Log(HealthSystem.GetHealthAmount());
        }
        #endregion

        #region Test
        [ContextMenu("TestMinus100Hp()")]
        private void TestMinus100Hp()
        {
            TakeDamage(110);
        }
        [ContextMenu("TestCollect10Element()")]
        private void TestCollect100Element()
        {
            entityElement.FireElement += 100;
            ElementChangeEvent();
        }
        [ContextMenu("Change Max Health")]
        private void TestChangeMaxHealth()
        {
            HealthSystem.ChangeMaxHealth(30);
        }
        #endregion
    }
}
