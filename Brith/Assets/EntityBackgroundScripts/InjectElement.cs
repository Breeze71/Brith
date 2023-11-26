using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class InjectElement : MonoBehaviour
    {
        /// <summary>
        /// 最大生成數量
        /// </summary>
        public int spawnAmountMax;
        
        /// <summary>
        /// 元素屬性
        /// </summary>
        [SerializeField] private List<ElementMovement> elementList;
        
        private Dictionary<int, ObjectPool> elementPoolDic = new Dictionary<int, ObjectPool>();



        public int[] POneInject;
        public float InitialSpeed;
        private bool flag = true;
        public int TimeOut;


        private void Awake() 
        {
            for(int i = 0; i < elementList.Count; i++)
            {
                elementPoolDic.Add(i, ObjectPool.CreateInstance(elementList[i], spawnAmountMax));
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                Fire();
            }
        }

        private PoolableObject DoSpawnElement(int _spawnIndex)
        {
            PoolableObject _poolableObject = elementPoolDic[_spawnIndex].GetObject();

            if(_poolableObject == null)
            {
                Debug.LogError("生成列表為空");
                return null;;
            }

            return _poolableObject;
        }

        private PoolableObject SpawnRandomElement()
        {
            return DoSpawnElement(Random.Range(0, elementList.Count));
        }


        void Fire()
        {
            int Pnumber = Random.Range(POneInject[0], POneInject[1]);
            float Particleangle = 360f / (Pnumber);

            for (int i = 0; i < Pnumber; i++)
            {
                GameObject _element = SpawnRandomElement().gameObject;
                _element.transform.position = gameObject.transform.position+ Quaternion.AngleAxis(Particleangle * i, Vector3.forward) * new Vector2(0, 0.5f);
                
                SetSpeed(Quaternion.AngleAxis(Particleangle*i,Vector3.forward)*new Vector2(0,1), _element);
            }
            flag = false;
        }
        void SetSpeed(Vector2 direction, GameObject particel)
        {
            Rigidbody2D tempRigidBody =particel.GetComponent<Rigidbody2D>();
            tempRigidBody.velocity = direction*InitialSpeed;
        }
        IEnumerator ThrowPartivles()
        {
            yield return new WaitForSeconds(TimeOut);
            flag = true;
        }
    }
}
