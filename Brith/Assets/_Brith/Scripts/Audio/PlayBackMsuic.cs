using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class PlayBackMsuic : MonoBehaviour
    {
        void Start()
        {
            int randomnum = Random.Range(0, 3);
            switch (randomnum)
            {
                case 0:
                    AudioManager.Instance.PlayLoop("Ambient01");
                    break;
                case 1:
                    AudioManager.Instance.PlayLoop("Ambient02");
                    break;
                case 2:
                    AudioManager.Instance.PlayLoop("Ambient03");
                    break;
            }
        }
    }
}
