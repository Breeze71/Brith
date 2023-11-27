using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class SpawnEntity : MonoBehaviour
    {
        [SerializeField] private Collider2D coll;
        [SerializeField] private GameObject[] entities;
        [SerializeField] private GameObject[] TargetEntity;
        [SerializeField] private GameObject[] Enemy;
        public RoomInfo room;
        public void CreateEntity()
        {
            room = gameObject.GetComponentInParent<RoomInfo>();
            if (room.EndRoom)
                EntitySpawnManager.Instance.SpawnEntities(coll, TargetEntity);
            if (room.RoomNumberFromOrigin != 0)
            {
                EntitySpawnManager.Instance.SpawnEntities(coll, Enemy, room.Number);
            }

            #region Spawn SceneEntity
            EntitySpawnManager.Instance.SpawnEntities(coll, entities);
            Room roomdata = NewRoommanagerOnGame.Instance.GetRoomList()[room.Number];
            roomdata.SceneEntityNumber = room.SceneEntity = roomdata.SceneEntityNumber + 1;
            #endregion
        }
    }
}
