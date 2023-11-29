using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace V
{
    public class TechButtonUI : MonoBehaviour
    {
        public Button Button;
        public GameObject ButtonMask;

        public void UnlockButton()
        {
            Button.gameObject.SetActive(true);
        }
    }
}
