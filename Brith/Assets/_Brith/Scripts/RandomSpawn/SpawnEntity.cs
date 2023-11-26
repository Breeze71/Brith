using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class SpawnEntity : MonoBehaviour
    {
        [SerializeField] private Collider2D coll;
        [SerializeField] private GameObject[] entities;
        
        private void Start() 
        {
            EntitySpawnManager.Instance.SpawnEntities(coll, entities);
        }
    }
}
