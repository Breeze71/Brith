using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCooldown : MonoBehaviour
{
    [HideInInspector]
    public bool flag;//Coling time
    public float CoolingTime;
    private void Awake()
    {
        flag = true;
    }
    public void Colling()
    {
        StartCoroutine(CoolingDown());
    }
    IEnumerator CoolingDown()
    {
        yield return new WaitForSeconds(CoolingTime);
        Debug.Log("COOL");  
        flag = true;
    }
}
