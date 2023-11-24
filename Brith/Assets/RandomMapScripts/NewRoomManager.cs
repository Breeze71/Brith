using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NewRoomManager : MonoBehaviour
{
    List<Room> RoomList;
    List<GameObject> Rooms = new();
    List<Vector3> RoomPosition;
    //Transform OriginPoint;
    Vector3 OriginPosition;
    public GameObject RoomPrefab;
    public float XOffsetMax;
    public float YOffsetMAX;
    [Header("房间总数量")]
    public int RoomNumber;
    [Header("房间最大间隔")]
    public int RoomIntervalMAX;//max interval between two rooms
    [Header("房间最小间隔")]
    public int RoomIntervalMIN;
    [Header("大房间数量")]
    public int BigRoomCount;
    [Header("大房间半径范围")]
    public int[] BigRoomRadius = new int[2];
    [Header("中房间数量")]
    public int MediumRoomCount;
    [Header("中房间半径范围")]
    public int[] MediumRoomRadius = new int[2];
    [Header("小房间数量")]
    public int SmallRoomCount;
    [Header("小房间半径范围")]
    public int[] SmallRoomRadius = new int[2];
    [Header("基础半径")]
    public int BaseRadius;

    #region IsVisableInCamera
    public Camera Camera;
    #endregion
    private void Start()
    {
        //OriginPoint = gameObject.transform;
        OriginPosition = gameObject.transform.position;
        //CreateNewRoom();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ClearRoom();
            CreateNewRoom();
            //test mst
            IcanSeeInCameral();
        }

    }
    #region IsVisableInCamera(Vector3 Position)
    /// <summary>
    /// to judge point if is in cameral
    /// </summary>
    public bool IsVisableInCamera(Vector3 Position)
    {
        //Debug.Log("IsVisableInCamera On");
        Vector3 viewPos = Camera.WorldToViewportPoint(Position);
        if (viewPos.x < 0 || viewPos.y < 0 || viewPos.x > 1 || viewPos.y > 1)
            return false;
        return true;
    }
    public bool IsVisableInCamera(Vector3 Position, float Radius)
    {
        List<float[]> Offset = new List<float[]>();
        Offset.Add(new[] { Radius, 0 });
        Offset.Add(new[] { 0, Radius });
        Offset.Add(new[] { -Radius, 0 });
        Offset.Add(new[] { 0, -Radius });

        Vector3 viewPos = Camera.WorldToViewportPoint(Position);
        if (viewPos.x < 0 || viewPos.y < 0 || viewPos.x > 1 || viewPos.y > 1)
            return false;
        for (int i = 0; i < 4; i++)
        {
            Vector3 tempPos = Camera.WorldToViewportPoint(new Vector3(Position.x + Offset[i][0], Position.y + Offset[i][1], 0));
            if (tempPos.x < 0 || tempPos.y < 0 || tempPos.x > 1 || tempPos.y > 1)
                return false;
        }
        return true;
    }
    #endregion
    #region IsCoincide(Vector3 position)
    /// <summary>
    /// is this room coincide with other rooms
    /// </summary>
    public bool IsCoincide(Vector3 position, float radius, List<Room> RoomList)
    {
        // Debug.Log("IsCoincide");
        foreach (Room v in RoomList)
        {
            float tempDis = Vector3.Distance(position, v.Position);
            if (tempDis <= radius + v.Radius | tempDis > RoomIntervalMAX)
                return false;
        }
        return true;
    }
    #endregion
    void CreateNewRoom()
    {
        RoomList = new List<Room>();

        #region to prevent stucking
        int KeyOut = 0;//to prevent stucking
        #endregion
        for (int i = 0; i < RoomNumber; i++)
        {
            Vector3 tempPos;
            while (true)
            {

                tempPos = new Vector3(OriginPosition.x + UnityEngine.Random.Range(-XOffsetMax, XOffsetMax), OriginPosition.y + UnityEngine.Random.Range(-YOffsetMAX, YOffsetMAX), 0);
                if (IsVisableInCamera(tempPos, BaseRadius) && IsCoincide(tempPos, BaseRadius, RoomList))
                {
                    //Debug.Log("Find");
                    break;
                }
                #region to prevent stucking
                KeyOut++;
                if (KeyOut > 100000)
                    break;
                #endregion
            }
            //save Room
            RoomList.Add(new(tempPos, 0.5f, i));
            Rooms.Add(Instantiate(RoomPrefab, tempPos, Quaternion.identity));
        }
        CreateMST(RoomList.Count);
    }
    void ClearRoom()
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            Destroy(Rooms[i]);
        }
        Rooms.Clear();
    }
    float DIstanceBTRoom(Room roomA, Room roomB)
    {
        return (Vector3.Distance(roomA.Position, roomB.Position));
    }
    void CreateMST(int number)
    {
        Graph g = new Graph(number);
        for (int i = 0; i < number; i++)
        {
            for (int j = 0; j < number; j++)
            {
                if (i != j)
                {
                    g.AddEdge(i, j, DIstanceBTRoom(RoomList[i], RoomList[j]));
                }
            }
        }
        List<Edge> mst = g.KruskalMST();
        foreach (Edge e in mst)
        {
            RoomList[e.Source].Connect(e.Destination);
            RoomList[e.Destination].Connect(e.Source);
        }
    }
    #region text mst
    void IcanSeeInCameral()
    {
        foreach (Room room in RoomList)
        {
            room.IcanSee();
        }
        for(int i = 0;i < Rooms.Count;i++)
        {
            GameObject go = Rooms[i];
            go.GetComponentInChildren<TMP_Text>().text = RoomList[i].IcanSeeWay;
        }
    }
    #endregion
}
