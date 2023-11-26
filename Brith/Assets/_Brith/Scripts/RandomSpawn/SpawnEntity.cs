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
        private void Start()
        {
            room = GetComponent<RoomInfo>();
            if (room.EndRoom)
                EntitySpawnManager.Instance.SpawnEntities(coll, TargetEntity);
            if (room.RoomNumberFromOrigin != 0)
                EntitySpawnManager.Instance.SpawnEntities(coll, Enemy);
            EntitySpawnManager.Instance.SpawnEntities(coll, entities);
        }
    }
}
