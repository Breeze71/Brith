using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRoomManager : MonoBehaviour
{
    List<Room> roomList;
    //Transform OriginPoint;
    Vector3 OriginPosition;
    public float XOffsetMax;
    public float YOffsetMAX;
    public int RoomNumber;

    #region IsVisableInCamera
    public Camera Camera;
    #endregion
    private void Start()
    {
        //OriginPoint = gameObject.transform;
        OriginPosition = gameObject.transform.position;
    }
    #region IsVisableInCamera(Vector3 Position)
    /// <summary>
    /// to judge point if is in cameral
    /// </summary>
    public bool IsVisableInCamera(Vector3 Position)
    {
        Vector3 viewPos = Camera.WorldToViewportPoint(Position);
        if (viewPos.x < 0 || viewPos.y < 0 || viewPos.x > 1 || viewPos.y > 1)
            return false;
        return true;
    }
    public bool IsVisableInCamera(Vector3 Position,int Radius)
    {
        Vector3 viewPos = Camera.WorldToViewportPoint(Position);
        if (viewPos.x < 0 || viewPos.y < 0 || viewPos.x > 1 || viewPos.y > 1)
            return false;
        for (int i = 0; i < 4; i++) { 

        }
        return true;
    }
    #endregion
    void CreateNewRoom()
    {
        roomList = new List<Room>();

        for (int i = 0; i < RoomNumber; i++)
        {
            while (IsVisableInCamera(new Vector3(OriginPosition.x + Random.Range(-XOffsetMax, XOffsetMax), OriginPosition.y + Random.Range(-YOffsetMAX, YOffsetMAX), 0)))
            {

            }
            //float XOffset = Random.Range(-XOffsetMax, XOffsetMax);
            //float YOffset = Random.Range(-YOffsetMAX, YOffsetMAX);
        }
    }
}
