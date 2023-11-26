using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public interface ITriggerCheckable
    {
        public bool IsAggroed {get; set;}
        public bool IsInAttackRange {get; set;}

        public void SetAggroStatus(bool _IsInChaseRange);

        public void SetAttackStatus(bool _IsInAttackRange);
    }
}
