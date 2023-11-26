using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public interface IEnemyMoveable
    {
        public Rigidbody2D Rb {get; set;}
        public bool IsFacingRight {get; set;}

        public void SetVelocity(Vector2 _velocity);
        public void CheckFacing(Vector2 _velocity);
    }
}
