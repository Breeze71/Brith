using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class SapwnCell : MonoBehaviour
    {
        [HideInInspector]
        public GameObject OriginRoom;
        public GameObject[] CellPrefab;
        public int CellSpawnNumber;
        void Start()
        {
            OriginRoom = NewRoommanagerOnGame.Instance.GetRooms()[0];
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider && hit.collider.gameObject!=null && hit.collider.gameObject.transform.position == OriginRoom.transform.position)
                {
                    Debug.Log("spawncell");
                    for (int i = 0; i < CellSpawnNumber; i++)
                    {
                        EntitySpawnManager.Instance.SpawnEntities(hit.collider, CellPrefab);
                    }
                }
            }
        }
    }
}
