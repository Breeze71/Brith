using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBackGround : MonoBehaviour
{
    public enum Attribute
    {
        None,
    }
    public Attribute attribute;
    [HideInInspector]
    public int ActiveCount;


    private void Awake()
    {
        ActiveCount = 0;
    }
}
