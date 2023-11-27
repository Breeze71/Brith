using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCooldown : MonoBehaviour
{
    [HideInInspector]
    public bool flag;//Coling time
    public float CoolingTime;
    public int RoomID;
    private void Awake()
    {
        flag = true;
        RoomID = 0;
    }
    public void Colling()
    {
        StartCoroutine(CoolingDown());
    }
    IEnumerator CoolingDown()
    {
        yield return new WaitForSeconds(CoolingTime);
        //Debug.Log("COOL");  
        flag = true;
    }
}
