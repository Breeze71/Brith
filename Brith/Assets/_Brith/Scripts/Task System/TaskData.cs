using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public enum ConditionType
    {
        Element,
        CellNumber,
        KillNumber,
        TimeLimit
    }
    [Serializable]
    public struct TaskConditions
    {
        public ConditionType conditionType;
        public int Count;
    }
    [Serializable]
    public struct Task
    {
        [Header("task description")]
        public string des;
        [Header("ID")]
        public string id;
        [Header("condition to complete task")]
        public TaskConditions taskConditions;
    }
    [Serializable]
    [CreateAssetMenu(fileName ="NewTaskDatabase",menuName ="CreateNewTaskDatabase/NewTaskDatabase")]
    public class TaskData : ScriptableObject
    {
        public List<Task> tasks;
        public ConditionType getConditionType()
        {
            return tasks[0].taskConditions.conditionType;
        }
    }
}
