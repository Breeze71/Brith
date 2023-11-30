using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomInfo : MonoBehaviour
{
    [HideInInspector]
    public int SceneEntity;
    [HideInInspector]
    public int RoomNumberFromOrigin;//the number of room from Originroom
    [HideInInspector]
    public bool EndRoom;//End this room is?
    public float Radius;
    public int Number;
    public List<int> ConnectedRoom = new List<int>();
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        //if (RoomNumberFromOrigin == 0) { 
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //    spriteRenderer.color = Color.cyan;
        //}

    }
}
