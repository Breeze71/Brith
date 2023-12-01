using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public static VfxManager Instance;
    public List<GameObject> VfxPrefab;
    private Dictionary<string, GameObject> effects = new Dictionary<string, GameObject>();
    private Dictionary<string, List<GameObject>> activeEffects = new Dictionary<string, List<GameObject>>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        foreach(GameObject obj in VfxPrefab)
        {
            RegisterEffect(obj.name, obj);
        }
    }
    //private void Update()
    //{
    //    if(Input.GetKeyUp(KeyCode.P))
    //    {
    //        PlayFire_explosion_air();
    //    }
    //}
    public void RegisterEffect(string effectName, GameObject effectPrefab)
    {
        if (!effects.ContainsKey(effectName))
        {
            effects.Add(effectName, effectPrefab);
        }
    }

    public void PlayEffect(string effectName, Vector3 position, float duration=1f)
    {
        if (effects.ContainsKey(effectName))
        {
            GameObject effectInstance = Instantiate(effects[effectName], position, Quaternion.identity);
            //if (activeEffects.ContainsKey(effectName))
            //{
            //    activeEffects[effectName].Add(effectInstance);
            //}
            //else
            //{
            //    activeEffects.Add(effectName, new List<GameObject> { effectInstance });
            //}
            StartCoroutine(StopEffectAfterDelay(effectInstance, duration));
        }
    }

    private IEnumerator StopEffectAfterDelay(GameObject go,float delay)
    {
        yield return new WaitForSeconds(delay);
        StopEffect(go);
    }

    public void StopEffect(GameObject effectPreab)
    {
        Destroy(effectPreab);
    }
    public void PlayFire_explosion_air()
    {
        PlayEffect("Fire_explosion_air", Camera.main.ScreenToWorldPoint(new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0)));
    }
}
