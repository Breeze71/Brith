using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    [CreateAssetMenu(fileName = "LvProfile", menuName = "Lv/LvProfile")]

    public class LvProfile : ScriptableObject
    {
        public int[] RoomNumber;
        public int[] OneRoomEnemyNumber;
        public int EndRoomEnemy;
        public int StartRoomSceneEntity;
        public int[] OneRoomSceneEntity;
    }
}
