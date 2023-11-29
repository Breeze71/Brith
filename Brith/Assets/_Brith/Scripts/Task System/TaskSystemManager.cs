using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class TaskSystemManager : MonoBehaviour
    {
        [SerializeField] private List<TaskData> tasks;
        List<TaskData> tasksOnGame;
        int task1_ID;
        int task2_ID;
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
            task1_ID = Random.Range(0, 4);
            task2_ID = Random.Range(0, 4);
            while (task1_ID==task2_ID)
            {
                task2_ID=Random.Range(0, 4);
            }
            tasksOnGame = new List<TaskData>();
            tasks.Add(tasks[task1_ID]);
            tasksOnGame.Add(tasks[task2_ID]);
        }
    }
}
