using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using V;

public class EntityBackGround : MonoBehaviour
{
    [SerializeField]
    private Element element;
    [HideInInspector]
    public int Roomid;
    public Element Getelement()
    {
        return element;
    }
}
