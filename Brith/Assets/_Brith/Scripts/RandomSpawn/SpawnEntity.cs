using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class SpawnEntity : MonoBehaviour
    {
        public GameObject Tip;
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
            entities = new List<GameObject[]>
            {
                FireOrigin,
                WaterOrigin,
                WindOrigin,
                GroundOrigin
            };
            room = gameObject.GetComponentInParent<RoomInfo>();
            //spawn target

            if (room.EndRoom)
            {
                //EntitySpawnManager.Instance.SpawnEntities(coll, TargetEntity, gameObject.transform.parent, new Vector2(room.transform.position.x, room.transform.position.y), room.Radius, 1f);
                GameObject go = Instantiate(TargetEntity[0], room.transform.position, Quaternion.identity);
                go.transform.SetParent(transform);
                int endENumber = ReadLv.Instance.lvData.EndRoomEnemy;
                // spawn enemy
                for (int i = 0; i < endENumber; i++)
                {
                    int randnum = Random.Range(0, 2);
                    EntitySpawnManager.Instance.SpawnEntities(coll, Enemy[randnum], room.Number);
                }
            }
            #region Spawn SceneEntity
            if (room.RoomNumberFromOrigin != 0)
            {
                int[] OneroomSNumber = ReadLv.Instance.lvData.OneRoomSceneEntity;
                int entityNumber = Random.Range(OneroomSNumber[0], OneroomSNumber[1] + 1);
                for (int i = 0; i < entityNumber; i++)
                {
                   // int randomNumber = Random.Range(0, 2);
                    EntitySpawnManager.Instance.SpawnEntities(coll, entities, gameObject.transform.parent, room.Number, new Vector2(room.transform.position.x, room.transform.position.y), room.Radius, 1f);
                    Room roomdata = NewRoommanagerOnGame.Instance.GetRoomList()[room.Number];
                    roomdata.SceneEntityNumber = room.SceneEntity = roomdata.SceneEntityNumber + 1;
                }

            }

            #endregion

            Debug.Log("Todo: spawn by lv");
            if (room.RoomNumberFromOrigin != 0 && !room.EndRoom)
            {
                int[] oneRoomeNuber = ReadLv.Instance.lvData.OneRoomEnemyNumber;
                int Odds = Random.Range(oneRoomeNuber[0], oneRoomeNuber[1] + 1);
                for (int i = 0; i < Odds; i++)
                {
                    int randnum = Random.Range(0, 2);
                    EntitySpawnManager.Instance.SpawnEntities(coll, Enemy[randnum], room.Number);
                }

            }
            if (room.RoomNumberFromOrigin == 0)
            {
                int SceneENumber = ReadLv.Instance.lvData.StartRoomSceneEntity;
                for(int i = 0; i < SceneENumber; i++)
                {
                    EntitySpawnManager.Instance.SpawnEntities(coll, entities, gameObject.transform.parent, room.Number, new Vector2(room.transform.position.x, room.transform.position.y), room.Radius, 1f);
                    Room roomdata = NewRoommanagerOnGame.Instance.GetRoomList()[room.Number];
                    roomdata.SceneEntityNumber = room.SceneEntity = roomdata.SceneEntityNumber + 1;
                }
                GameObject go = Instantiate(Tip, room.transform.position, Quaternion.identity);
                go.transform.Translate(0, 0.3f, 0);
                go.transform.localScale *= room.Radius / 0.6f;
                StartCoroutine(twinkle(go));
            }
        }

        IEnumerator twinkle(GameObject go)
        {
            //SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
            for (int i = 0; i <= 500; i++)
            {
                if(!go)
                    yield break;
                //Debug.Log("aaaaa");
                go.transform.localScale *= 2f;
                yield return new WaitForSeconds(0.2f);
                go.transform.localScale /= 2f;
                yield return new WaitForSeconds(0.2f);
            }
            go.SetActive(false);
            Destroy(go);
        }
    }
}
