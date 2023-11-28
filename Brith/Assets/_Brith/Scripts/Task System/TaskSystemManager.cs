using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class TaskSystemManager : MonoBehaviour
    {
        public static TaskSystemManager Instance { get; set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one TaskSystemManager");
            }

            Instance = this;
        }

    }
}
