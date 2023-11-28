using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    /// <summary>
    /// Spawm Entity Instance Class
    /// </summary>
    public class EntitySpawnManager : MonoBehaviour
    {
        public static EntitySpawnManager Instance { get; set; }

        [SerializeField] private LayerMask layerNotToSpawn;
        [SerializeField, Tooltip("用於避免生成在存在的 entity 上 ")] private float entitySize;
        [SerializeField] private float offest;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one EntitySpawnManager");
            }

            Instance = this;
        }

        /// <summary>
        /// Spawn
        /// </summary>
        public void SpawnEntities(Collider2D _spawnArea, GameObject[] _entities)
        {
            foreach (GameObject _entity in _entities)
            {
                Vector2 _spawnPosition = GetRandomSpawnPosition(_spawnArea);
                GameObject spawnEntity = Instantiate(_entity, _spawnPosition, Quaternion.identity);
            }
        }
        //spawn target specially
        public void SpawnEntities(Collider2D _spawnArea, GameObject[] _entities, Transform father)
        {
            foreach (GameObject _entity in _entities)
            {
                Vector2 _spawnPosition = GetRandomSpawnPosition(_spawnArea);
                GameObject spawnEntity = Instantiate(_entity, _spawnPosition, Quaternion.identity);
                spawnEntity.transform.SetParent(father);
            }
        }
        //sapwn enemy specially
        public void SpawnEntities(Collider2D _spawnArea, GameObject[] _entities, int _number)
        {
            foreach (GameObject _entity in _entities)
            {
                Vector2 _spawnPosition = GetRandomSpawnPosition(_spawnArea);
                GameObject spawnEntity = Instantiate(_entity, _spawnPosition, Quaternion.identity);
                spawnEntity.GetComponent<TransformCooldown>().RoomID = _number;
            }
        }
        //spawn scene entity specially
        public void SpawnEntities(Collider2D _spawnArea, List<GameObject[]> _entities, Transform father, int _number)
        {
            int randomNumber = Random.Range(0, 4);
            int randomNumberOutlook = Random.Range(0, 2);
            if(randomNumberOutlook >= _entities[randomNumber].Length)
            {
                randomNumberOutlook= 0;
            }
            GameObject _entity = _entities[randomNumber][randomNumberOutlook];
            Vector2 _spawnPosition = GetRandomSpawnPosition(_spawnArea);
            GameObject spawnEntity = Instantiate(_entity, _spawnPosition, Quaternion.identity);
            spawnEntity.transform.SetParent(father);
            EntityBackGround entityBackGround = spawnEntity.GetComponent<EntityBackGround>();
            entityBackGround.Roomid = _number;
        }
        /// <summary>
        /// Get a Random position whitout collision with other layer
        /// </summary>
        private Vector2 GetRandomSpawnPosition(Collider2D _spawnArea)
        {
            Vector2 _spawnPosition = Vector2.zero;
            bool _isSpawnPositionValid = false;

            int _attempCount = 0;
            int _maxAttemp = 50;

            // 嘗試 50 次 隨機生成點
            while (!_isSpawnPositionValid && _attempCount < _maxAttemp)
            {
                _spawnPosition = GetRandomPointInCollider(_spawnArea, offest);
                Collider2D[] _colls = Physics2D.OverlapCircleAll(_spawnPosition, 2f);

                bool _isInValidCollision = false;
                foreach (Collider2D _coll in _colls)
                {
                    if (((1 << _coll.gameObject.layer) & layerNotToSpawn.value) != 0)
                    {
                        _isInValidCollision = true;
                        break;
                    }
                }

                if (!_isInValidCollision)
                {
                    _isSpawnPositionValid = true;
                }

                _attempCount++;
            }

            // 50 次都沒法生成
            if (!_isSpawnPositionValid)
            {
                Debug.LogWarning("Could not find a spawn position");
            }

            return _spawnPosition;
        }


        /// <summary>
        /// Cacu the offest, prevent spawn on wall
        /// </summary>
        private Vector2 GetRandomPointInCollider(Collider2D _coll, float _offest = 1f)
        {
            Bounds _collBounds = _coll.bounds;

            Vector2 minBounds = new Vector2(_collBounds.min.x + _offest, _collBounds.min.y + _offest);
            Vector2 maxBounds = new Vector2(_collBounds.max.x - _offest, _collBounds.max.y - _offest);

            float _randomX = Random.Range(minBounds.x, maxBounds.x);
            float _randomY = Random.Range(minBounds.y, maxBounds.y);

            return new Vector2(_randomX, _randomY);
        }
    }
}
