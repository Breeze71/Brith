using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        #region spawn target specially
        public void SpawnEntities(Collider2D _spawnArea, GameObject[] _entities, Transform father)
        {
            foreach (GameObject _entity in _entities)
            {
                Vector2 _spawnPosition = GetRandomSpawnPosition(_spawnArea);
                GameObject spawnEntity = Instantiate(_entity, _spawnPosition, Quaternion.identity);
                spawnEntity.transform.SetParent(father);
            }
        }
        public void SpawnEntities(Collider2D _spawnArea, GameObject[] _entities, Transform father, Vector2 position, float radius, float offset)
        {
            foreach (GameObject _entity in _entities)
            {
                //Vector2 _spawnPosition = GetRandomSpawnPosition(_spawnArea);
                Vector2 _spawnPosition = GetGetRandomSpawnPosition(position, radius, offset);
                GameObject spawnEntity = Instantiate(_entity, _spawnPosition, Quaternion.identity);
                spawnEntity.transform.SetParent(father);
            }
        }
        #endregion
        #region sapwn enemy specially
        public void SpawnEntities(Collider2D _spawnArea, GameObject _entity, int _number)
        {
            Vector2 _spawnPosition = GetRandomSpawnPosition(_spawnArea);
            GameObject spawnEntity = Instantiate(_entity, _spawnPosition, Quaternion.identity);
            spawnEntity.GetComponent<TransformCooldown>().RoomID = _number;
        }
        #endregion
        #region spawn scene entity specially
        public void SpawnEntities(Collider2D _spawnArea, List<GameObject[]> _entities, Transform father, int _number)
        {
            int randomNumber = Random.Range(0, 4);
            int randomNumberOutlook = Random.Range(0, 2);
            if (randomNumberOutlook >= _entities[randomNumber].Length)
            {
                randomNumberOutlook = 0;
            }
            GameObject _entity = _entities[randomNumber][randomNumberOutlook];
            Vector2 _spawnPosition = GetRandomSpawnPosition(_spawnArea);
            GameObject spawnEntity = Instantiate(_entity, _spawnPosition, Quaternion.identity);
            spawnEntity.transform.SetParent(father);
            EntityBackGround entityBackGround = spawnEntity.GetComponent<EntityBackGround>();
            entityBackGround.Roomid = _number;
        }
        public void SpawnEntities(Collider2D _spawnArea, List<GameObject[]> _entities, Transform father, int _number, Vector2 position, float radius, float offset)
        {
            int randomNumber = Random.Range(0, 4);
            int randomNumberOutlook = Random.Range(0, 2);
            if (randomNumberOutlook >= _entities[randomNumber].Length)
            {
                randomNumberOutlook = 0;
            }
            GameObject _entity = _entities[randomNumber][randomNumberOutlook];
            Vector2 _spawnPosition = GetGetRandomSpawnPosition(position, radius, offset);
            GameObject spawnEntity = Instantiate(_entity, _spawnPosition, Quaternion.identity);
            spawnEntity.transform.SetParent(father);
            EntityBackGround entityBackGround = spawnEntity.GetComponent<EntityBackGround>();
            entityBackGround.Roomid = _number;
        }
        #endregion
        /// <summary>
        /// Get a Random position whitout collision with other layer
        /// </summary>
        private Vector2 GetGetRandomSpawnPosition(Vector2 spawnPosition, float radius, float offset = 0.3f)
        {
            int key = 0;
            int maxkey = 500;
            Vector2 Position;
            while (true)
            {
                bool _isInValidCollision = true;
                float x = spawnPosition.x + Random.Range(-radius + offset, radius - offset);
                float y = spawnPosition.y + Random.Range(-radius + offset, radius - offset);
                Position = new Vector2(x, y);
                Collider2D[] _colls = Physics2D.OverlapCircleAll(Position, 0.8f);
                foreach (Collider2D _coll in _colls)
                {
                    if (((1 << _coll.gameObject.layer) & layerNotToSpawn.value) != 0)
                    {
                        _isInValidCollision = false;
                        break;
                    }
                }
                key++;
                if (key > maxkey | _isInValidCollision)
                {
                   // Debug.LogWarning(key);
                    break;
                }

            }

            return Position;
        }
        private Vector2 GetRandomSpawnPosition(Collider2D _spawnArea)
        {
            Vector2 _spawnPosition = Vector2.zero;
            bool _isSpawnPositionValid = false;

            int _attempCount = 0;
            int _maxAttemp = 500;

            // 嘗試 500 次 隨機生成點
            while (!_isSpawnPositionValid && _attempCount < _maxAttemp)
            {
                _spawnPosition = GetRandomPointInCollider(_spawnArea, offest);
                Collider2D[] _colls = Physics2D.OverlapCircleAll(_spawnPosition, 1f);

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
                    break;
                }
                _attempCount++;
            }

            // 500 次都沒法生成
            if (!_isSpawnPositionValid)
            {
                Debug.LogWarning("Could not find a spawn position");
            }

            return _spawnPosition;
        }


        /// <summary>
        /// Cacu the offest, prevent spawn on wall
        /// </summary>
        private Vector2 GetRandomPointInCollider(Collider2D _coll, float _offest)
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
