using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector3 Position;
    public float Radius;
    public int Number;
    List<int> ConnectedRoom = new List<int>();
    public string IcanSeeWay;
    public Room(Vector3 position, float radius, int number)
    {
        Position = position;
        Radius = radius;
        Number = number;
        IcanSeeWay = number.ToString() + ":";//test
    }
    public void Connect(int connectedRoom)
    {
        ConnectedRoom.Add(connectedRoom);
    }
    //test mst
    public void IcanSee()
    {
        for (int i = 0; i < ConnectedRoom.Count; i++)
        {
            int room = ConnectedRoom[i];
            if (i == ConnectedRoom.Count - 1)
                IcanSeeWay += room;
            else
                IcanSeeWay += room + "/";
        }
    }
    public List<int> GetConnectedRoom
    {
        get { return ConnectedRoom; }
    }
}
