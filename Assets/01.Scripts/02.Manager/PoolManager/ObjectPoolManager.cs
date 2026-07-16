using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;


[Serializable]
public class PoolData
{
    public GameObject Prefab;
    public int InitSpawnCount;
    public Transform parentTrans;
}

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializedDictionary("Pool Name", "Pool Data")]
    public SerializedDictionary<string, PoolData> prefabDic = new SerializedDictionary<string, PoolData>();
    protected Dictionary<string, Queue<GameObject>> activatedPool = new();

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
                activatedPool[o.Key] = new Queue<GameObject>();
                GameObject parentPool = new GameObject($"{o.Key}");
                parentPool.transform.SetParent(this.transform);
                prefabDic[o.Key].parentTrans = parentPool.transform;
                
                

                for (int i = 0; i < o.Value.InitSpawnCount; i++)
                {
                    GameObject go = Instantiate(o.Value.Prefab, o.Value.parentTrans);
                    go.SetActive(false);
                    activatedPool[o.Key].Enqueue(go);
                }
            }
        }
    }

    // 풀에 넣은 프리팹이 없을 때 호출하게 되는 경우 prefDic에 새로운 풀을 넣는 예외처리 추가해야함
    public GameObject GetObject(string poolObjectName)
    {
        GameObject effect = null;

        if (activatedPool.TryGetValue(poolObjectName, out var objectPool))
        {
            Debug.Log($"{objectPool} TryGetValue");
            if (objectPool.Count == 0)
            {
                effect = Instantiate(prefabDic[poolObjectName].Prefab, prefabDic[poolObjectName].parentTrans);
            }
            else
            {
                effect = objectPool.Dequeue();
            }
        }
        else
        {
            // 예외 처리 넣어야함
            // 풀을 만들든 에러 문구 뜨든
            Debug.LogError("pool 실패");
        }
        effect.SetActive(true);
        return effect;
    }

    public void ReturnObject(string effectPoolName, GameObject objectToReturn)
    {
        if (!activatedPool.ContainsKey(effectPoolName))
        {
            Destroy(objectToReturn); 
            return;
        }

        objectToReturn.SetActive(false);
        activatedPool[effectPoolName].Enqueue(objectToReturn);
    }
}