using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    Vector3 Position;
    float Radius;
    int Number;
    List<int> ConnectedRoom;
    Room(Vector3 position, float radius, int number)
    {
        Position = position;
        Radius = radius;
        Number = number;
    }
    public void Connect(int connectedRoom)
    {
        ConnectedRoom.Add( connectedRoom);
    }
}
