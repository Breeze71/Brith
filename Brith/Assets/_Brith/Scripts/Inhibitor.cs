using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using V._Core;
using V.UI;

namespace V
{
    public class Inhibitor : MonoBehaviour, IDamagable
    {
        [field : SerializeField] public int HealthAmount { get; set; }
        public HealthSystem HealthSystem { get; set; }
        public HealthBarUI HealthBarUI { get; set; }


        [SerializeField] private GameObject techTreeUI;

        private void Start() 
        {
            HealthSystem = new HealthSystem(HealthAmount);
            techTreeUI = GameObject.FindGameObjectWithTag("TechTree");

            techTreeUI.SetActive(false);
        }

        public void TakeDamage(int damageAmount)
        {
            HealthSystem.TakeDamage(damageAmount);
            
            Debug.Log("Inhibitor damage" + HealthSystem.GetHealthAmount());
            if(HealthSystem.GetHealthAmount() <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Player Win
        /// </summary>
        public void Die()
        {
            techTreeUI.SetActive(true);
        }

        [ContextMenu("test dead")]
        private void TestDead()
        {
            TakeDamage(200);
        }
    }
}
