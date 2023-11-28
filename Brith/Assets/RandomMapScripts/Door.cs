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
    public string EnemyTag;
    private void OnTriggerStay2D(Collider2D collision)
    {
        var TransformInfo = collision.gameObject.GetComponent<TransformCooldown>();
        if (TransformInfo != null)
        {
            if ((collision.tag == ParticleTag && TransformInfo.flag) | (collision.tag == Marblestag && TransformInfo.flag) | (collision.tag == EnemyTag && TransformInfo.flag))
            {
                TransformInfo.flag = false;
                collision.gameObject.transform.position = EndPosition;
                TransformInfo.RoomID = ConnectedRoom;
                if(collision.gameObject.activeSelf)
                    TransformInfo.Colling();
            }
        }

        //if (collision.tag == ParticleTag  | collision.tag == Marblestag)
        //{
        //    Debug.Log(EndPosition);
        //    collision.gameObject.transform.position = EndPosition;
        //}
    }
}
