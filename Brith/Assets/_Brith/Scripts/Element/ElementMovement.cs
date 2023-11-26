using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class ElementMovement : PoolableObject
    {
        [SerializeField] private float minStableSpeed;

        private Rigidbody2D rb;
        private Vector2 moveDiretion;

        private void Awake() 
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Start() 
        {
            // Vector2 _randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            // rb.AddForce(_randomDirection * minStableSpeed, ForceMode2D.Impulse);
            // moveDiretion = _randomDirection;
        }

        private void Update() 
        {
            // // if current velocity less then minStable Speed
            // if(rb.velocity.magnitude < minStableSpeed * 0.7)
            // {
            //     rb.AddForce(moveDiretion * minStableSpeed, ForceMode2D.Impulse);
            // }

            // // if current velocity more than minStable speed
            // else if(rb.velocity.magnitude > minStableSpeed * 1.2)
            // {
            //     Vector2 _currentDir = rb.velocity.normalized;

            //     rb.velocity = new Vector2(_currentDir.x, _currentDir.y) * minStableSpeed;         
            // }
        }

        public override void OnDisable()
        {
            base.OnDisable();

            rb.velocity = Vector2.zero;
        }
    }
}
