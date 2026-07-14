using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;


[Serializable]
public struct PoolData<T> where T : MonoBehaviour
{
    public T Prefab; 
    public int InitSpawnCount;
}

abstract public class ObjectPoolManager<T> : Singleton<ObjectPoolManager<T>> where T : MonoBehaviour
{
    [SerializedDictionary("Pool Name", "Pool Data")]
    public SerializedDictionary<string, PoolData<T>> Pool = new();
    protected Dictionary<string, Queue<T>> activatedPool = new();

    protected void InitPool()
    {
        foreach (var o in Pool)
        {
            if (!activatedPool.ContainsKey(o.Key))
            {
                Queue<T> objectPool = new Queue<T>();

                for (int i = 0; i < o.Value.InitSpawnCount; i++)
                {
                    var obj = CreatePooledObject(o.Value.Prefab, o.Key);
                    objectPool.Enqueue(obj);
                }

                activatedPool[o.Key] = objectPool;
            }
        }
    }
    protected abstract T CreatePooledObject(T prefab, string poolName);

    public T Get(string poolObjectName)
    {
        T effect = null;

        if (activatedPool.TryGetValue(poolObjectName, out var objectPool))
        {
            effect = objectPool.Count > 0 ? objectPool.Dequeue()
                : CreatePooledObject(Pool[poolObjectName].Prefab, poolObjectName);

            effect.gameObject.SetActive(true);
        }

        return effect;
    }

    public void Return(string effectPoolName, T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        activatedPool[effectPoolName].Enqueue(objectToReturn);
    }
}