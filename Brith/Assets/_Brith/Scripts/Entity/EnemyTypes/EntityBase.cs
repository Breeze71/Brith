using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

namespace V
{
    public class EntityBase : MonoBehaviour, IEnemyMoveable, ITriggerCheckable, IDamagable
    {
        public Rigidbody2D Rb {get; set;}
        public bool IsFacingRight {get; set;} = true;

        #region IDamagable
        [field : SerializeField] public HealthBarUI HealthBarUI { get; set; }
        public HealthSystem HealthSystem { get; set;}
        public int HealthAmount { get; set; }
        #endregion

        public Image CellImg;
        public EntityElement EntityElement{ get; set;}   // 存儲元素 
        public int Attack;
        public float Speed;
        public int Defense;
        public int MaxHealth;

        protected event Action OnElementChange; // Element Change
        /// <summary>
        /// invoke when Collect Element
        /// </summary>
        public void ElementChangeEvent()
        {
            OnElementChange?.Invoke();
        }

        #region ScriptableObject FSM
        [SerializeField] private EnemyIdleSOBase enemyIdleSOBase;
        [SerializeField] private EnemyChaseSOBase enemyChaseSOBase;
        [SerializeField] private EnemyAttackSOBase enemyAttackSOBase;

        public EnemyIdleSOBase EnemyIdleBaseInstance {get; set;}
        public EnemyChaseSOBase EnemyChaseBaseInstance {get; set;}
        public EnemyAttackSOBase EnemyAttackBaseInstance {get; set;}        
        #endregion
        
        #region FSM
        public EnemyStateMachine StateMachine{get; set;}

        public EnemyIdleState IdleState {get; set;}
        public EnemyAttackState AttackState {get; set;}
        public EnemyChaseState ChaseState {get; set;}
        #endregion

        #region FSM - var
        public Transform TargetTransform;
        #endregion

        #region Unity
        private void Awake() 
        {
            // PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            EnemyIdleBaseInstance = Instantiate(enemyIdleSOBase);
            EnemyChaseBaseInstance = Instantiate(enemyChaseSOBase);
            EnemyAttackBaseInstance = Instantiate(enemyAttackSOBase);

            StateMachine = new EnemyStateMachine();

            IdleState = new EnemyIdleState(this, StateMachine);    
            AttackState = new EnemyAttackState(this, StateMachine);   
            ChaseState = new EnemyChaseState(this, StateMachine);

            InitalizeEntity();
        }
        protected virtual void Start() 
        {
            Rb = GetComponent<Rigidbody2D>();    

            EnemyIdleBaseInstance.Initialize(gameObject, this);
            EnemyChaseBaseInstance.Initialize(gameObject, this);
            EnemyAttackBaseInstance.Initialize(gameObject, this);

            StateMachine.Initalize(IdleState);

            SetEntity();
        }
        
        private void Update() 
        {
            StateMachine.CurrentEnemyState.FrameUpdate();    
        }
        private void FixedUpdate() 
        {
            StateMachine.CurrentEnemyState.PhysicsUpdate();
        }
        #endregion

        /// <summary>
        /// Call at Awake
        /// </summary>
        public virtual void InitalizeEntity() {}
        /// <summary>
        /// Call at Start
        /// </summary>
        protected virtual void SetEntity(){}

        #region Movement
        public void SetVelocity(Vector2 _velocity)
        {
            Rb.velocity = _velocity;
            CheckFacing(_velocity);
        }

        public void CheckFacing(Vector2 _velocity)
        {
            if(IsFacingRight && _velocity.x < 0f)
            {
                Flip();
            }
            else if(!IsFacingRight && _velocity.x > 0)
            {
                Flip();
            }
        }
        private void Flip()
        {
            Vector3 _rotate = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(_rotate);
            IsFacingRight = !IsFacingRight;                  
        }
        #endregion
    
        #region ITrigger
        public bool IsAggroed {get; set;}
        public bool IsInAttackRange {get; set;}

        public void SetAggroStatus(bool _IsInChaseRange)
        {
            IsAggroed = _IsInChaseRange;
        }

        public void SetAttackStatus(bool _IsInAttackRange)
        {
            IsInAttackRange = _IsInAttackRange;
        }
        #endregion

        #region Anim Trigger
        private void AnimTriggerEvent(AnimTriggerTypes _triggerTypes)
        {
            StateMachine.CurrentEnemyState.AnimTriggerEvent(_triggerTypes);
        }

        public enum AnimTriggerTypes
        {
            EnemyDamaged,
            PlayFootStepSound,
        }
        #endregion
    
        #region GearSysytem
        public GearInfo gearInfo;
        private bool GearFull;
        private List<Element> Gears = new List<Element>();

        public List<Element> GetGears()
        {
            return Gears;
        }
        protected void GearSysrem()
        {
            if (!GearFull) 
            { 
                GearFull = IsGaearFull();
                ToCreateWhichGear();
            }
        }
        bool IsGaearFull()
        {
            if(Gears.Count>=4)
                return true;
            return false;
        }
        void ToCreateWhichGear()
        {
            if (Gears.Count < 2)
            {
                if (EntityElement.FireElement >= gearInfo.FireArmCost)
                {
                    EntityElement.FireElement -= gearInfo.FireArmCost;
                    Gears.Add(Element.Fire);
                    //todo Hp+=gearInfo.GroundArmEffect;
                    
                    // To - Do
                    // attack 
                    Attack += 10;
                    // defense
                    Defense += 10;
                    // health
                    HealthSystem.ChangeMaxHealth(10);
                    // speed
                    Speed += 10;

                }
                if (EntityElement.GroundElement >= gearInfo.GroundArmCost)
                {
                    EntityElement.GroundElement -= gearInfo.GroundArmCost;
                    Gears.Add(Element.Ground);
                    //todo Hp+=gearInfo.GroundArmEffect;
                }
                if (EntityElement.WaterElement >= gearInfo.WaterArmCost)
                {
                    EntityElement.WaterElement -= gearInfo.WaterArmCost;
                    Gears.Add(Element.Water);
                    //todo Def+=gearInfo.WaterArmEffect;
                }
                if (EntityElement.WindElement >= gearInfo.WindArmCost)
                {
                    EntityElement.WindElement -= gearInfo.WindArmCost;
                    Gears.Add(Element.Wind);
                    //todo Spd+=gearInfo.WindArmEffect;
                }
            }
            else if (Gears.Count >= 2 && Gears.Count < 4) {
                if (EntityElement.FireElement >= gearInfo.FireLegCost)
                {
                    EntityElement.FireElement -= gearInfo.FireLegCost;
                    Gears.Add(Element.Fire);
                    //todo attack+=gearInfo.FireLegEffect;
                }
                if (EntityElement.GroundElement >= gearInfo.GroundLegCost)
                {
                    EntityElement.GroundElement -= gearInfo.GroundLegCost;
                    Gears.Add(Element.Ground);
                    //todo Hp+=gearInfo.GroundLegEffect;
                }
                if (EntityElement.WaterElement >= gearInfo.WaterLegCost)
                {
                    EntityElement.WaterElement -= gearInfo.WaterLegCost;
                    Gears.Add(Element.Water);
                    //todo Def+=gearInfo.WaterLegEffect;
                }
                if (EntityElement.WindElement >= gearInfo.WindLegCost)
                {
                    EntityElement.WindElement -= gearInfo.WindLegCost;
                    Gears.Add(Element.Wind);
                    //todo Spd+=gearInfo.WindLegEffect;
                }
            }
            
        }
        #endregion
    
        #region IDamagable
        public virtual void TakeDamage(int _attack){}

        public virtual void Die(){}
        #endregion
    }
}
