using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class GameEventManager : MonoBehaviour
    {
        public static GameEventManager Instance { get; private set; }

        public PlayerEvent PlayerEvent;
        public SkillEvent SkillEvent;

        private void Awake() 
        {
            if(Instance != null)
            {
                Debug.LogError("More than One GameEvent Manager");
            }    
            Instance = this;

            PlayerEvent = new PlayerEvent();
            SkillEvent = new SkillEvent();
        }
    }
}
