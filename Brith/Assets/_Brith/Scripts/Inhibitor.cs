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
        [field :SerializeField]public HealthBarUI HealthBarUI { get; set; }


        [SerializeField] private GameObject techTreeUI;
        private CellTech cellTech;

        private void Awake() 
        {
            HealthSystem = new HealthSystem(HealthAmount);
            techTreeUI = GameObject.FindGameObjectWithTag("TechTree");
            cellTech = GameObject.FindGameObjectWithTag("CellTag").GetComponent<CellTech>();
        }
        private void Start() 
        {
            HealthBarUI.SetupHealthSystemUI(HealthSystem);
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

            cellTech.GetTechPoint(TaskSystemManager.Instance.GetAllstarNum());

            cellTech.currentLevel += 1;
        }

        [ContextMenu("TestMinus50Hp()")]
        private void TestMinus50Hp()
        {
            TakeDamage(50);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}
