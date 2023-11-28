using UnityEngine;

namespace V
{
    public class EntityBase : MonoBehaviour, IEnemyMoveable, ITriggerCheckable
    {
        public Rigidbody2D Rb {get; set;}
        public bool IsFacingRight {get; set;} = true;
        public int attack;
        public float speed;

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
    }
}
