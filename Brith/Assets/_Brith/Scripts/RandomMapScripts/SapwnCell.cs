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
        public int SpawnCellTimeMax;
        private int CurrentSpawnTime;
        //public GameObject[] CellPrefab2;

        public int CellSpawnNumber;
        void Start()
        {
            CurrentSpawnTime = 0;
            //OriginRoom = NewRoommanagerOnGame.Instance.GetRooms()[0];
        }
        void Update()
        {
            if (CurrentSpawnTime<SpawnCellTimeMax)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (hit.collider && hit.collider.gameObject != null && hit.collider.gameObject.transform.position == OriginRoom.transform.position)
                    {
                        //Debug.Log("spawncell");
                        if (CurrentSpawnTime == 0) {
                            GameObject go = GameObject.Find("Tip(Clone)");
                            go.SetActive(false);
                            Destroy(go);
                        }

                        CurrentSpawnTime++;
                        for (int i = 0; i < CellSpawnNumber; i++)
                        {
                            int Randnum = Random.Range(0, 3);
                            Instantiate(CellPrefab[Randnum], hit.point, Quaternion.identity);
                        }
                    }
                }
            }

            //if(Input.GetMouseButtonDown(1) )
            //{
            //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //    if (hit.collider && hit.collider.gameObject != null && hit.collider.gameObject.transform.position == OriginRoom.transform.position)
            //    {
            //        Debug.Log("spawnEnemy");
            //        for (int i = 0; i < CellSpawnNumber; i++)
            //        {
            //            Instantiate(CellPrefab2[0], hit.point, Quaternion.identity);
            //        }
            //    }
            //}
        }
    }
}
