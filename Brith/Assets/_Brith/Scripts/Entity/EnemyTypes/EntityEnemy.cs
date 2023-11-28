using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class EntityEnemy : EntityBase, IDamagable
    {
        #region IDamagable
        [field : SerializeField] public HealthBarUI HealthBarUI { get; set; }
        public HealthSystem HealthSystem { get; set;}
        public int HealthAmount { get; set; }
        #endregion

        #region Element && Reproduce
        [SerializeField] private int elementAmountToReproduce = 200;
        [SerializeField] private GameObject element;
        [SerializeField] private GameObject enemyOriginCell;
        public EntityElement entityElement{ get; set;}   // 存儲元素 

        private event Action OnElementChange; // Element Change
        #endregion

        #region Skill
        public float SlowdownSpeed = 1f;   // 钟慢效应 敌方细胞的移速
        public float SlowdownTimerMax = 3f; // 钟慢效应 时间
        public float EndlessTimerMax; // 生生不息 时间
        [SerializeField] private GameObject entityOriginCell; // 生命涌现 我方的初始细胞
        private bool isSpringup = false;
        private bool isEndless;
        #endregion

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

            OnElementChange += BasicEntity_ElementChange;  // pick up element
            GameEventManager.Instance.SkillEvent.OnSlowdownSkill += SkillEvent_OnSlowdownSkill;
            GameEventManager.Instance.SkillEvent.OnSpringUpSkill += SkillEvent_OnSpringUpSkill;
            GameEventManager.Instance.SkillEvent.OnEndlessSkill += SkillEvent_OnEndlessSkill;
        }

        private void OnDestroy() 
        {
            OnElementChange -= BasicEntity_ElementChange;  // pick up element

            GameEventManager.Instance.SkillEvent.OnSlowdownSkill -= SkillEvent_OnSlowdownSkill;
            GameEventManager.Instance.SkillEvent.OnSpringUpSkill -= SkillEvent_OnSpringUpSkill;
            GameEventManager.Instance.SkillEvent.OnEndlessSkill -= SkillEvent_OnEndlessSkill;
        }
        #endregion

        #region Health
        public void TakeDamage(int _attack)
        {
            int _countDamage = _attack - Defense;

            HealthSystem.TakeDamage(_countDamage);

            if(HealthSystem.GetHealthAmount() <= 0)
            {
                Die();
            }
        }

        public void Die()
        {   
            // 生生不息
            if(isEndless)
            {
                return;
            }

            // TO - DO 每有五十点元素结晶就变成一个随机方向的新元素粒子。
            int totalElement = entityElement.GetTotalElementAmount();

            while(totalElement >= 50)
            {
                Instantiate(element);

                totalElement -= 50;
            }

            // 生命涌现
            if(isSpringup)
            {
                Vector3 spawnPosition = transform.position;

                GameObject newEntity = Instantiate(entityOriginCell, spawnPosition, Quaternion.identity);

                newEntity.transform.parent = null;
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

                GameObject newEntity = Instantiate(enemyOriginCell, spawnPosition, Quaternion.identity);

                newEntity.transform.parent = null;
            }
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
    
        #region Skill 钟慢效应
        private void SkillEvent_OnSlowdownSkill()
        {
            StartCoroutine(Coroutine_SlowDownSpeed(Speed, SlowdownSpeed));
        }

        private IEnumerator Coroutine_SlowDownSpeed(float _originEnemySpeed, float _slowdownSpeed)
        {
            Speed = _slowdownSpeed;
            yield return new WaitForSeconds(SlowdownTimerMax);
            Speed = _originEnemySpeed;
        }
        #endregion

        #region Skill 生命涌现
        private void SkillEvent_OnSpringUpSkill()
        {
            isSpringup = true;
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
