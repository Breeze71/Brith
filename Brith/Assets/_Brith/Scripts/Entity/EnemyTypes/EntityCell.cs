using System;
using System.Collections;
using UnityEngine;

namespace V
{
    public class EntityCell : EntityBase, IDamagable
    {   
        #region IDamagable
        [field : SerializeField] public HealthBarUI HealthBarUI { get; set; }
        public HealthSystem HealthSystem { get; set;}
        public int HealthAmount { get; set; }
        #endregion

        #region Element && Reproduce
        [SerializeField] private int elementAmountToReproduce = 200;
        [SerializeField] private GameObject element;
        [SerializeField] private GameObject entityOriginCell; // 生命涌现 我方的初始细胞
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

        #region Skill
        public float EndlessTimerMax; // 生生不息 时间
        private bool isEndless = false;
        #endregion

        private CellTech cellTech;

        #region Unity
        public override void InitalizeEntity()
        {
            base.InitalizeEntity();

            HealthSystem = new HealthSystem(MaxHealth);
            entityElement = new EntityElement();
        }
        protected override void SetEntity()
        {
            HealthBarUI.SetupHealthSystemUI(HealthSystem);


            cellTech = GameObject.FindGameObjectWithTag("CellTag").GetComponent<CellTech>();           
            cellTech.OnUnlockedNewTech += CellTech_OnUnlockedNewTech; // add new tech
            cellTech.CheckUnlockSkill();

            OnElementChange += BasicEntity_ElementChange;  // pick up element

            GameEventManager.Instance.PlayerEvent.SpawnCellEvent(); // 通知 manager 生成新細胞

            GameEventManager.Instance.SkillEvent.OnEndlessSkill += SkillEvent_OnEndlessSkill;
        }
        private void OnDestroy() 
        {
            GameEventManager.Instance.PlayerEvent.CellDeadEvent(); // 通知 manager 細胞死亡
            GameEventManager.Instance.SkillEvent.OnEndlessSkill -= SkillEvent_OnEndlessSkill;
            
            cellTech.OnUnlockedNewTech -= CellTech_OnUnlockedNewTech; // add new tech
            OnElementChange -= BasicEntity_ElementChange;  // pick up element
        }
        #endregion

        #region Health
        public void TakeDamage(int _attack)
        {
            // 生生不息
            if(isEndless)
            {
                return;
            }

            int _countDamage = _attack - Defense;

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

                // 生成同位置，並不為父子物體
                Vector3 spawnPosition = transform.position;

                GameObject newEntity = Instantiate(entityOriginCell, spawnPosition, Quaternion.identity);

                newEntity.transform.parent = null;
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
            Speed += _speed;

            Debug.Log(Speed);
        }
        private void SetMaxHealthAmount(int _amount)
        {
            HealthSystem.ChangeMaxHealth(MaxHealth + _amount);

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
    
        #region Skill 生生不息
        private void SkillEvent_OnEndlessSkill()
        {
            StartCoroutine(Coroutine_Endless());
        }    
        private IEnumerator Coroutine_Endless()
        {
            isEndless = true;
            yield return new WaitForSeconds(EndlessTimerMax);
            isEndless = false;
        }
        #endregion
    }
}
