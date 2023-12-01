using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class ResolutionManager : MonoBehaviour
    {
        public int[] Screen1;
        public int[] Screen2;
        public int[] Screen3;
        public int[] Screen4;
        public void SetScreen1()
        {
            Screen.SetResolution(Screen1[0], Screen1[1], false);
            //Debug.Log("1111");
        }
        public void SetScreen2()
        {
            Screen.SetResolution(Screen2[0], Screen2[1], false);
        }
        public void SetScreen3()
        {
            Screen.SetResolution(Screen3[0], Screen3[1], false);
        }
        public void SetScreen4()
        {
            Screen.SetResolution(Screen4[0], Screen4[1], false);
        }
    }
}
