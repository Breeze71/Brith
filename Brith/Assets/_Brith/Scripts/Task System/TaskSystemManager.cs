using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace V
{
    public class TaskSystemManager : MonoBehaviour
    {
        class taskID
        {
            public GameObject task;
            public int ID;
            public taskID(GameObject task, int id)
            {
                this.task = task;
                ID = id;
            }
        }
        Dictionary<ConditionType, taskID> Task = new Dictionary<ConditionType, taskID>()
        {
            {ConditionType.KillNumber, null },
            {ConditionType.CellNumber, null },
            {ConditionType.TimeLimit, null },
            {ConditionType.Element, null }
        };
        List<int> StarNumber = new List<int>();

        [SerializeField] private List<GameObject> StarObjects1;
        [SerializeField] private List<GameObject> StarObject2;
        List<List<GameObject>> StarObjects;
        [SerializeField] private List<GameObject> TaskMenu;
        [SerializeField] private List<TaskData> tasks;
        [SerializeField] private List<TaskData> tasksOnGame;
        int task1_ID;
        int task2_ID;
        #region task condition
        [HideInInspector]
        public int CellNumber;
        [HideInInspector]
        public int CollectElementNumber;
        [HideInInspector]
        public int KillNumber;
        #endregion
        public static TaskSystemManager Instance { get; set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one TaskSystemManager");
            }

            Instance = this;
            StarObjects = new List<List<GameObject>>
            {
                StarObjects1,
                StarObject2
            };
        }
        private void Start()
        {
            task1_ID = UnityEngine.Random.Range(0, 4);
            task2_ID = UnityEngine.Random.Range(0, 4);
            while (task1_ID == task2_ID)
            {
                task2_ID = UnityEngine.Random.Range(0, 4);
            }
            tasksOnGame = new List<TaskData>
            {
                tasks[task1_ID],
                tasks[task2_ID]
            };
            #region 
            for (int i = 0; i < tasksOnGame.Count; i++)
            {
                if (tasksOnGame[i].getConditionType() == ConditionType.TimeLimit)
                {
                    Task[ConditionType.TimeLimit] = new(TaskMenu[i], i);
                    StarNumber.Add(3);
                }
                else if (tasksOnGame[i].getConditionType() == ConditionType.KillNumber)
                {
                    Task[ConditionType.KillNumber] = new taskID(TaskMenu[i], i);
                    StarNumber.Add(0);
                }
                else if (tasksOnGame[i].getConditionType() == ConditionType.Element)
                {
                    Task[ConditionType.Element] = new taskID(TaskMenu[i], i);
                    StarNumber.Add(0);
                }
                else if (tasksOnGame[i].getConditionType() == ConditionType.CellNumber)
                {
                    Task[ConditionType.CellNumber] = new(TaskMenu[i], i);
                    StarNumber.Add(0);
                }

            }
            #endregion
            updateCellNumber(0);
            updateCollectElementNumber(0);
            updateKillNumber(0);
        }
        public float timer = 0f;

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            updateTime((int)timer);
        }
        #region interface for lv end
        public int GetAllstarNum()
        {
            int num = 0;
            foreach (int i in StarNumber)
                num += i;
            return num;
        }
        #endregion
        void updateCellNumber(int cellnumber)
        {
            CellNumber = cellnumber;
            Debug.Log(cellnumber);
            updateUI(ConditionType.CellNumber, cellnumber);
        }
        void updateCollectElementNumber(int collectelementnumber)
        {
            CollectElementNumber = collectelementnumber;
            updateUI(ConditionType.Element, collectelementnumber);
        }
        void updateKillNumber(int killnumber)
        {
            KillNumber = killnumber;
            updateUI(ConditionType.KillNumber, killnumber);
        }
        void updateTime(int time)
        {
            updateUIForTime(ConditionType.TimeLimit, time);
        }
        #region update ui for plus
        void updateUI(ConditionType condition, int number)
        {
            if (Task[condition] != null)
            {
                int id = Task[condition].ID;
                GameObject go = Task[condition].task;
                TaskData task = tasksOnGame[id];
                String des = task.tasks[0].des;
                if (number <= task.tasks[0].taskConditions.Count && StarNumber[id] == 0)
                {
                    //StarNumber[id] = 0;
                    go.GetComponentInChildren<TMP_Text>().text = des + " " + number + "/" + task.tasks[0].taskConditions.Count;
                    if (number >= task.tasks[0].taskConditions.Count)
                    {
                        StarNumber[id] = 1;
                        StarObjects[id][StarNumber[id] - 1].GetComponent<UnityEngine.UI.Image>().color = Color.white;
                        go.GetComponentInChildren<TMP_Text>().text = des + " " + number + "/" + task.tasks[1].taskConditions.Count;
                    }

                }
                else if (number >= task.tasks[0].taskConditions.Count && number <= task.tasks[1].taskConditions.Count && (StarNumber[id] >=0 & StarNumber[id]<=1))
                {
                    go.GetComponentInChildren<TMP_Text>().text = des + " " + number + "/" + task.tasks[1].taskConditions.Count;
                    if (number >= task.tasks[1].taskConditions.Count)
                    {
                        StarNumber[id] = 2;
                        StarObjects[id][StarNumber[id] - 1].GetComponent<UnityEngine.UI.Image>().color = Color.white;
                        go.GetComponentInChildren<TMP_Text>().text = des + " " + number + "/" + task.tasks[2].taskConditions.Count;
                    }
                }
                else if (StarNumber[id] >= 0)
                {
                    go.GetComponentInChildren<TMP_Text>().text = des + " " + number + "/" + task.tasks[2].taskConditions.Count;
                    if (number >= task.tasks[2].taskConditions.Count && StarNumber[id] >=0)
                    {
                        StarNumber[id] = 3;
                        foreach(GameObject gameObject in StarObjects[id])
                            gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                    }
                }
            }
        }
        #endregion
        #region update ui for minus
        void updateUIForTime(ConditionType condition, int number)
        {
            if (Task[condition] != null)
            {
                int id = Task[condition].ID;
                GameObject go = Task[condition].task;
                TaskData task = tasksOnGame[id];
                String des = task.tasks[0].des;
                if (number <= task.tasks[2].taskConditions.Count&& StarNumber[id]==3)
                {
                    foreach(GameObject goo in StarObjects[id])
                        goo.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                    //StarNumber[id] = 0;
                    go.GetComponentInChildren<TMP_Text>().text = des + " " + number + "/" + task.tasks[2].taskConditions.Count;
                    if (number >= task.tasks[2].taskConditions.Count)
                    {
                        StarNumber[id] = 2;
                        StarObjects[id][2].GetComponent<UnityEngine.UI.Image>().color = Color.black;
                        go.GetComponentInChildren<TMP_Text>().text = des + " " + number + "/" + task.tasks[1].taskConditions.Count;
                    }

                }
                else if (number >= task.tasks[2].taskConditions.Count && number <= task.tasks[1].taskConditions.Count && StarNumber[id]==2)
                {
                    go.GetComponentInChildren<TMP_Text>().text = des + " " + number + "/" + task.tasks[1].taskConditions.Count;
                    if (number == task.tasks[1].taskConditions.Count)
                    {
                        StarNumber[id] = 1;
                        StarObjects[id][1].GetComponent<UnityEngine.UI.Image>().color = Color.black;
                        go.GetComponentInChildren<TMP_Text>().text = des + " " + number + "/" + task.tasks[0].taskConditions.Count;
                    }
                }
                else if (StarNumber[id] <= 1)
                {
                    go.GetComponentInChildren<TMP_Text>().text = des + " " + number + "/" + task.tasks[0].taskConditions.Count;
                    if (number >= task.tasks[0].taskConditions.Count)
                    {
                        StarNumber[id] = 0;
                        StarObjects[id][0].GetComponent<UnityEngine.UI.Image>().color = Color.black;
                    }
                }
            }
        }
        #endregion
        

        #region test
        [ContextMenu("cllnumber=50")]
        void Cellnumber50()
        {
            updateCellNumber(5);
        }
        #endregion
    }

}
