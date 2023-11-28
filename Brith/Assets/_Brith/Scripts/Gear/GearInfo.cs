using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    [CreateAssetMenu(fileName = "GearInfo", menuName = "Gear/GearInfo")]
    public class GearInfo : ScriptableObject
    {
        public int FireArmCost;
        public int WindArmCost;
        public int GroundArmCost;
        public int WaterArmCost;
        public int FireLegCost;
        public int WindLegCost;
        public int GroundLegCost;
        public int WaterLegCost;
        public int FireArmEffect;
        public int WindArmEffect;
        public int GroundArmEffect;
        public int WaterArmEffect;
        public int FireLegEffect;
        public int WindLegEffect;
        public int GroundLegEffect;
        public int WaterLegEffect;
    }
}
