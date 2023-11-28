using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class SpawnEntity : MonoBehaviour
    {
        [SerializeField] private Collider2D coll;
        [SerializeField] private GameObject[] FireOrigin;
        [SerializeField] private GameObject[] WaterOrigin;
        [SerializeField] private GameObject[] WindOrigin;
        [SerializeField] private GameObject[] GroundOrigin;
        private List<GameObject[]> entities;
        [SerializeField] private GameObject[] TargetEntity;
        [SerializeField] private GameObject[] Enemy;
        public RoomInfo room;
        public void CreateEntity()
        {
            //Debug.Log(FireOrigin[1].name);
            entities = new List<GameObject[]>();
            entities.Add(FireOrigin);
            entities.Add(WaterOrigin);
            entities.Add(WindOrigin);
            entities.Add(GroundOrigin);
            room = gameObject.GetComponentInParent<RoomInfo>();
            //spawn target
            if (room.EndRoom)
            {
                EntitySpawnManager.Instance.SpawnEntities(coll, TargetEntity, gameObject.transform.parent);
            }
            //spawn scene entity
            if (room.RoomNumberFromOrigin != 0)
            {
                EntitySpawnManager.Instance.SpawnEntities(coll, Enemy, room.Number);
            }

            #region Spawn SceneEntity
            int randomNumber = Random.Range(0, 2);
            EntitySpawnManager.Instance.SpawnEntities(coll, entities, gameObject.transform.parent, room.Number);
            Room roomdata = NewRoommanagerOnGame.Instance.GetRoomList()[room.Number];
            roomdata.SceneEntityNumber = room.SceneEntity = roomdata.SceneEntityNumber + 1;
            #endregion
        }
    }
}
