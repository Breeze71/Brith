using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injectparticle : MonoBehaviour
{
    public GameObject Particleprefab;
    [HideInInspector]
    public int ActiveParticleNumber;
    [Header("一次发射粒子的数量范围(min,max)")]
    public int[] POneInject;
    [Header("发射间隔时间")]
    public int TimeOut;
    [Header("最大粒子存在数")]
    public int Maxnumber;
    [Header("粒子发射初速度")]
    public float InitialSpeed;

    bool flag=true;

    #region Objectpool
    //private ObjectPool pool;
    #endregion

    //private void Start()
    //{
    //    pool = FindObjectOfType<ObjectPool>();
    //    
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Fire();
        }
    }

    //private void HandleObjectActivated(GameObject activator, GameObject obj)
    //{
    //    if (activator.transform == gameObject.transform)
    //    {
    //        ActiveCount++;
    //        //// 物体被此激活源激活
    //        //Debug.Log("物体被激活: " + obj.name);
    //    }
    //}

    //private void HandleObjectDeactivated(GameObject activator, GameObject obj)
    //{
    //    if (activator.transform == gameObject.transform)
    //    {
    //        ActiveCount--;
    //        OnDestory();
    //        //// 物体被此激活源取消激活
    //        //Debug.Log("物体被取消激活: " + obj.name);
    //    }
    //}

    //private void OnDestroy()
    //{
    //    // 取消订阅事件
    //    if (pool != null)
    //    {
    //        pool.OnObjectActivated -= HandleObjectActivated;
    //        pool.OnObjectDeactivated -= HandleObjectDeactivated;
    //    }
    //}
    void Fire()
    {
        int Pnumber = Random.Range(POneInject[0], POneInject[1]);
        float Particleangle = 360f / (Pnumber);
        //Debug.Log(Particleangle);
        for (int i = 0; i < Pnumber; i++)
        {
            //!!!here is the position when instantiate
            // 订阅事件
            //    pool.OnObjectActivated += HandleObjectActivated;
            //    pool.OnObjectDeactivated += HandleObjectDeactivated;
            GameObject particle =Instantiate(Particleprefab,gameObject.transform.position+ Quaternion.AngleAxis(Particleangle * i, Vector3.forward) * new Vector2(0, 0.5f), Quaternion.identity);//TODO: get from objectpool
            SetSpeed(Quaternion.AngleAxis(Particleangle*i,Vector3.forward)*new Vector2(0,1), particle);
        }
        flag = false;
    }
    void SetSpeed(Vector2 direction, GameObject particel)
    {
        Rigidbody2D tempRigidBody =particel.GetComponent<Rigidbody2D>();
        tempRigidBody.velocity = direction*InitialSpeed;
    }
    IEnumerator ThrowPartivles()
    {
        yield return new WaitForSeconds(TimeOut);
        flag = true;
    }
}
