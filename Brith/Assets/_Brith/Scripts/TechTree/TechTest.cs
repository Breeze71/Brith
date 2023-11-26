using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using V.UI;

namespace V
{
    public class TechTest : MonoBehaviour
    {
        [SerializeField] private EntityCell entityCell;
        [SerializeField] private TechTreeUI techTreeUI;

        private void Start()
        {
            techTreeUI.SetCellTech(entityCell.GetCellTech());
        }
    }
}
