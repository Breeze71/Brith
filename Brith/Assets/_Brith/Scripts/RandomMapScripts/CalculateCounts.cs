using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace V
{
    public class CalculateCounts : MonoBehaviour
    {
        RoomInfo roomInfo;
        int EnemyNumber;
        int CellNumber;
        int ElementNumber;
        float EnemyPowerAll;
        float CellPowerAll;

        private void Start()
        {
            roomInfo = GetComponent<RoomInfo>();
        }
        private void Update()
        {

            //Collider2D[] collider2D= Physics2D.OverlapCircleAll(gameObject.transform.position, roomInfo.Radius);
        }
    }
}
