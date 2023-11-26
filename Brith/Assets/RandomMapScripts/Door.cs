using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [HideInInspector]
    public int ConnectedRoom;
    [HideInInspector]
    public Vector3 EndPosition;
    public string ParticleTag;
    public string Marblestag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var TransformInfo = collision.gameObject.GetComponent<TransformCooldown>();
        if ((collision.tag== ParticleTag && TransformInfo.flag) | (collision.tag == Marblestag && TransformInfo.flag))
        {
            TransformInfo.flag = false;
            TransformInfo.Colling();
            collision.gameObject.transform.position = EndPosition;
        }
    }
}
