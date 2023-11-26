using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class SpawnTest : MonoBehaviour
    {
        [SerializeField] private Collider2D coll;
        [SerializeField] private GameObject[] entities;

        [ContextMenu("Spawn")]
        private void Spawn()
        {
            EntitySpawnManager.Instance.SpawnEntities(coll, entities);
        }

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Spawn();

                
            }
        }
    }
}
