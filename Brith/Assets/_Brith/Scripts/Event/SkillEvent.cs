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
            Debug.Log("To - Do - ElementBurstSkillEvent()");
        }

        public event Action OnSlowdownSkill;
        /// <summary>
        /// 钟慢效应
        /// </summary>
        public void SlowdownSkillEvent()
        {
            OnSlowdownSkill?.Invoke();
        }


        public event Action OnEndlessSkill;
        /// <summary>
        /// 生生不息
        /// </summary>
        public void EndlessSkillEvent()
        {
            OnEndlessSkill?.Invoke();
        }        

        public event Action OnSpringUpSkill;
        /// <summary>
        /// 生命涌现
        /// </summary>
        public void SpringUpEvent()
        {
            OnSpringUpSkill?.Invoke();
        }        
    }
}
