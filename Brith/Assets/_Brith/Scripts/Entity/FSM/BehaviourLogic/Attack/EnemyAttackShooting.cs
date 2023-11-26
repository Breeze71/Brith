using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    [CreateAssetMenu(fileName = "Shooting", menuName = "EnemySO/Attack/Shoot", order = 0)]
    public class EnemyAttackShooting : EnemyAttackSOBase
    {
        [SerializeField] private Rigidbody2D bulletPrefab;
        [SerializeField] private float shootCDTimer;
        [SerializeField] private float shootCDTimerMax = 2f;
        [SerializeField] private float exitTimer;
        [SerializeField] private float exitTimerMax = 3f;
        [SerializeField] private float distanceToCountExit = 6f;
        [SerializeField] private float bulletSpeed = 5f;


        public override void DoEnterState()
        {
            base.DoEnterState();

            enemyBase.SetVelocity(Vector2.zero);
        }
        public override void DoFrameUpdate()
        {
            base.DoFrameUpdate();
            enemyBase.SetVelocity(Vector2.zero);
            
            if(shootCDTimer > shootCDTimerMax)
            {
                shootCDTimer = 0f;
                Vector2 _dir =  (targetTransform.position - enemyBase.transform.position).normalized;

                Rigidbody2D _bullet = GameObject.Instantiate(bulletPrefab, enemyBase.transform.position, Quaternion.identity);
                _bullet.velocity = _dir * bulletSpeed;
            }   

            // 待修改
            if(Vector2.Distance(targetTransform.position, enemyBase.transform.position) >= distanceToCountExit)
            {
                exitTimer += Time.deltaTime;

                if(exitTimer > exitTimerMax)
                {
                    enemyBase.StateMachine.ChangeState(enemyBase.ChaseState);
                }
            }
            else
            {
                exitTimer = 0f;
            }

            shootCDTimer += Time.deltaTime;               
        }
    }
}
