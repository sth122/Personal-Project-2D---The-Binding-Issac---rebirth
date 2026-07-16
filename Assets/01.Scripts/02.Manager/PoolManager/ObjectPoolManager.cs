using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;


[Serializable]
public struct PoolData
{
    public GameObject Prefab;
    public int InitSpawnCount;
}

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializedDictionary("Pool Name", "Pool Data")]
    public SerializedDictionary<string, PoolData> prefabDic = new SerializedDictionary<string, PoolData>();
    protected Dictionary<string, Queue<GameObject>> activatedPool = new();
    //public List<GameObject> listGameObj = new List<GameObject>();

    private void Awake()
    {
        InitPool();
    }


    public void InitPool()
    {
        foreach (var o in prefabDic)
        {
            if (!activatedPool.ContainsKey(o.Key))
            {
                //Queue<GameObject> objectPool = new Queue<GameObject>();
                activatedPool[o.Key] = new Queue<GameObject>();
                GameObject parentPool = new GameObject($"{o.Key}");
                parentPool.transform.SetParent(this.transform);

                for (int i = 0; i < o.Value.InitSpawnCount; i++)
                {
                    GameObject go = Instantiate(o.Value.Prefab, parentPool.transform);
                    go.SetActive(false);
                    activatedPool[o.Key].Enqueue(go);
                    //activatedPool[o.Key].Enqueue(go);
                    //CreatePooledObject(o.Value.Prefab, o.Key);
                    //CreatePooledObject(go.name);
                }
                // activatedPool[o.Key] = new Queue<GameObject>(); 로 선언해도 되는거 아닌가?
                //activatedPool[o.Key] = objectPool;
            }
        }
    }

    //public void CreatePooledObject(GameObject prefab, string poolName)
    public void CreatePooledObject(string poolName)
    {
        GameObject go;

        go = prefabDic[poolName].Prefab;
        if (go != null)
        {
            activatedPool[poolName].Enqueue(go);
        }
        else
        {
            Debug.Log("Pool null error");
        }

        //foreach (GameObject obj in listGameObj)
        //{
        //    if (obj.name == poolName)
        //    {
        //        go = obj.GetComponent<GameObject>();
        //        activatedPool[poolName].Enqueue(go);
        //    }
        //}
    }

    public GameObject GetObject(string poolObjectName)
    {
        GameObject effect = null;

        if (activatedPool.TryGetValue(poolObjectName, out var objectPool))
        {
            if (objectPool.Count == 0)
            {
                //CreatePooledObject(prefabDic[poolObjectName].Prefab, poolObjectName);
                //CreatePooledObject(poolObjectName);
                effect = Instantiate(prefabDic[poolObjectName].Prefab, transform.Find(poolObjectName));
                //activatedPool[poolObjectName].Enqueue(effect);
            }
            else
            {
                effect = objectPool.Dequeue();
            }

            //effect.gameObject.SetActive(true);
            
        }
        effect.SetActive(true);
        return effect;
    }

    public void ReturnObject(string effectPoolName, GameObject objectToReturn)
    {
        //objectToReturn.gameObject.SetActive(false);
        if (!activatedPool.ContainsKey(effectPoolName))
        {
            Destroy(objectToReturn); 
            return;
        }

        objectToReturn.SetActive(false);
        activatedPool[effectPoolName].Enqueue(objectToReturn);
    }
}