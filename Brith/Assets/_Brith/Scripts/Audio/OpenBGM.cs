using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class OpenBGM : MonoBehaviour
    {
        void Start()
        {
            AudioManager.Instance.PlayLoop("OceanBGM");
        }
    }
}
