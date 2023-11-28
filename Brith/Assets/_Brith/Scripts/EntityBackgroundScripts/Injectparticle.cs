using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injectparticle : MonoBehaviour
{
    public GameObject Particleprefab;
    [HideInInspector]
    public int ActiveParticleNumber;
    [Header("һ�η������ӵ�������Χ(min,max)")]
    public int[] POneInject;
    [Header("������ʱ��")]
    public int TimeOut;
    [Header("������Ӵ�����")]
    public int Maxnumber;
    [Header("���ӷ�����ٶ�")]
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
    //        //// ���屻�˼���Դ����
    //        //Debug.Log("���屻����: " + obj.name);
    //    }
    //}

    //private void HandleObjectDeactivated(GameObject activator, GameObject obj)
    //{
    //    if (activator.transform == gameObject.transform)
    //    {
    //        ActiveCount--;
    //        OnDestory();
    //        //// ���屻�˼���Դȡ������
    //        //Debug.Log("���屻ȡ������: " + obj.name);
    //    }
    //}

    //private void OnDestroy()
    //{
    //    // ȡ�������¼�
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
            // �����¼�
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
