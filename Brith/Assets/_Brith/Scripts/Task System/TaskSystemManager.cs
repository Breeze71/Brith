using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class TaskSystemManager : MonoBehaviour
    {
        [SerializeField] private List<TaskData> tasks;
        public static TaskSystemManager Instance { get; set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one TaskSystemManager");
            }

            Instance = this;
        }
        private void Start()
        {
            int task1_ID = Random.Range(0, 4);
            int task2_ID = Random.Range(0, 4);
            while (task1_ID==task2_ID)
            {
                task2_ID=Random.Range(0, 4);
            }
        }
    }
}
