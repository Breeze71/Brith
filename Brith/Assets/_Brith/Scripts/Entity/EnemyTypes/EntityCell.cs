using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class EntityCell : EntityBase, IDamagable
    {
        #region Element && Reproduce
        [SerializeField] private int elementAmountToReproduce = 200;
        [SerializeField] private GameObject element;
        [SerializeField] private GameObject entityOriginCell; // 生命涌现 我方的初始细

        public event Action OnReproduce;
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
            EntityElement = new EntityElement();
        }
        protected override void SetEntity()
        {
            HealthBarUI.SetupHealthSystemUI(HealthSystem);

            cellTech = GameObject.FindGameObjectWithTag("CellTag").GetComponent<CellTech>();           
            cellTech.OnUnlockedNewTech += CellTech_OnUnlockedNewTech; // add new tech

            foreach(TechType techType in cellTech.unlockTechList)
            {
                CellTech_OnUnlockedNewTech(techType);
            }

            OnElementChange += BasicEntity_ElementChange;  // pick up element

            TaskSystemManager.Instance.updateCellNumber(1);

            GameEventManager.Instance.SkillEvent.OnEndlessSkill += SkillEvent_OnEndlessSkill;
            GameEventManager.Instance.PlayerEvent.SpawnCellEvent();
        }
        private void OnDestroy() 
        {
            GameEventManager.Instance.PlayerEvent.CellDeadEvent(); // 通知 manager 細胞死亡
            GameEventManager.Instance.SkillEvent.OnEndlessSkill -= SkillEvent_OnEndlessSkill;
            

            cellTech.OnUnlockedNewTech -= CellTech_OnUnlockedNewTech; // add new tech
            OnElementChange -= BasicEntity_ElementChange;  // pick up element

            TaskSystemManager.Instance.updateCellNumber(-1);
        }
        #endregion

        #region Health
        public override void TakeDamage(int _attack)
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

        public override void Die()
        {
            // TO - DO 每有五十点元素结晶就变成一个随机方向的新元素粒子。
            int totalElement = EntityElement.GetTotalElementAmount();

            while(totalElement >= 50)
            {
                Instantiate(element);

                totalElement -= 50;
            }

            // To - Do Cell Dead


            Destroy(gameObject);
        }
        #endregion

        #region Reproduce
        private void BasicEntity_ElementChange()
        {
            // Create Gear First
            GearSysrem();

            // To - Do 細胞每次獲得 n 個 element
            //  += collectElementAmount;
            TaskSystemManager.Instance.updateCollectElementNumber(collectElementAmount);

            // 檢查元素總量
            if(EntityElement.GetTotalElementAmount() >= elementAmountToReproduce)
            {
                EntityElement.DecreaseElement();

                // 生成同位置，並不為父子物體
                Vector3 spawnPosition = transform.position;

                GameObject newEntity = Instantiate(entityOriginCell, spawnPosition, Quaternion.identity);

                newEntity.transform.parent = null;
            }
        }

        #endregion
    
        #region Tech
        [Header("Tech")]
        public int Hp_1 = 4;
        public int Hp_2 = 10;
        public int Atk_1 = 2;
        public int Atl_2 = 5;
        public float Spd_1 = .2f;
        public float Spd_2 = .5f;
        public int Def_1 = 5;

        public int elementCollectPlusAmount = 5;
        /// <summary>
        /// Recive Add Tech Event 
        /// </summary>
        private void CellTech_OnUnlockedNewTech(TechType techType)
        {
            // To - Do Lot of Tech
            switch(techType)
            {   
                #region 數值
                case TechType.Hp_1_Plus4:
                    SetMaxHealthAmount(Hp_1);
                    Debug.Log(HealthSystem.GetHealthAmount());
                    break;

                case TechType.Hp_2_Plus10:
                    SetMaxHealthAmount(Hp_2);
                    Debug.Log(HealthSystem.GetHealthAmount());
                    break;

                case TechType.Atk_1_Plus2:
                    Attack += Atk_1;
                    Debug.Log("atk" + Attack);
                    break;

                case TechType.Atk_2_Plus5:
                    Attack += Atl_2;
                    Debug.Log("atk" + Attack);
                    break;    

                case TechType.Spd_1_Plus10:
                    Speed += Spd_1;
                    Debug.Log("spd" + Speed);
                    break;

                case TechType.Spd_2_Plus20:
                    Speed += Spd_2;
                    Debug.Log("spd" + Speed);
                    break; 

                case TechType.Def_1_Plus5:
                    Defense += Def_1;
                    Debug.Log("def" + Defense);
                    break;
                #endregion

                case TechType.Element_1_Plus5:
                    collectElementAmount += elementCollectPlusAmount;
                    break;
            }
        }

        public CellTech GetCellTech()
        {
            return cellTech;
        }

        /// <summary>
        /// 會不會追逐敵人
        /// </summary>
        /// <returns></returns>
        public bool CanChaseEnemy()
        {
            return cellTech.IsTechUnlocked(TechType.CellChaseEnemy);
        }
        /// <summary>
        /// 會不會追元素
        /// </summary>
        /// <returns></returns>
        public bool CanChaseElement()
        {
            return cellTech.IsTechUnlocked(TechType.CellChaseElement);
        }

        private void SetMaxHealthAmount(int _amount)
        {
            HealthSystem.ChangeMaxHealth(_amount);

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
            EntityElement.FireElement += 100;
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
