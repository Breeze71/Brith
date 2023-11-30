using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V.Tool.SaveLoadSystem
{
    public class GameData
    {
        public List<TechType> UnlockTechList;
        public int CurrentTechPoint;
        public int MaxTechPoint;
        public int CurrentLevel;

        public GameData()
        {
            UnlockTechList = new List<TechType>();

            CurrentTechPoint = 1;
            MaxTechPoint = 1;
            CurrentLevel = 1;
        }
    }
}
