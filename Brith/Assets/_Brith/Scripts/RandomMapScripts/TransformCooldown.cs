using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using V;

public class TransformCooldown : MonoBehaviour
{
    [HideInInspector]
    public bool flag;//Coling time
    public float CoolingTime;
    public float StartCoolingTime;
    public int RoomID;
    bool HasSendMessage;
    private void Awake()
    {
        flag = true;
        RoomID = 0;
    }
    private void Start()
    {
        CalculateCounts calculateCounts = NewRoommanagerOnGame.Instance.GetRooms()[RoomID].GetComponent<CalculateCounts>();
        //calculateCounts.
    }
    public void Colling()
    {
        if (gameObject.activeSelf)
            StartCoroutine(CoolingDown());
        else
            flag = true;
    }
    IEnumerator CoolingDown()
    {
        yield return new WaitForSeconds(CoolingTime);
        //Debug.Log("COOL");  
        flag = true;
    }
    IEnumerator StartCoolingDown() {
        yield return new WaitForSeconds(StartCoolingTime);
        flag = true;
    }
}
