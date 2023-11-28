using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class SkillEvent
    {
        public event Action OnElementBurst;
        /// <summary>
        /// 元素迸发
        /// </summary>
        public void ElementBurstSkillEvent()
        {
            OnElementBurst?.Invoke();
            Debug.Log("ElementBurstSkillEvent()");
        }

        public event Action OnSlowdownSkill;
        /// <summary>
        /// 钟慢效应
        /// </summary>
        public void SlowdownSkillEvent()
        {
            OnSlowdownSkill?.Invoke();
            Debug.Log("SlowdownSkillEvent()");
        }


        public event Action OnEndlessSkill;
        /// <summary>
        /// 生生不息
        /// </summary>
        public void EndlessSkillEvent()
        {
            OnEndlessSkill?.Invoke();
            Debug.Log("EndlessSkillEvent()");
        }        

        public event Action OnSpringUpSkill;
        /// <summary>
        /// 生命涌现
        /// </summary>
        public void SpringUpEvent()
        {
            OnSpringUpSkill?.Invoke();
            Debug.Log("SpringUpEvent()");
        }        
    }
}
