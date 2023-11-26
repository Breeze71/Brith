using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCooldown : MonoBehaviour
{
    [HideInInspector]
    public bool flag;//Coling time
    public float CoolingTime;
    public void Colling()
    {
        StartCoroutine(CoolingDown());
    }
    IEnumerator CoolingDown()
    {
        yield return new WaitForSeconds(CoolingTime);
        flag = true;
    }
}
