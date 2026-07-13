using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IReturnPoolable
{
    public void ReturnPool();
}

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    #region Variable 
    [SerializeField] List<GameObject> objList = new List<GameObject>();
    Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
    int poolSize;
    #endregion


    /// <summary>
    /// DDOL로 인한 init 기능
    /// GameScene에서만 pool init
    /// </summary>
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (scene.buildIndex != 2)
    //    {
    //        ClearPool();
    //    }
    //    else
    //    {
    //        ClearPool();
    //        SetupPool();
    //    }
    //}

    protected override void Initialize()
    {
        poolSize = 1;

        foreach (GameObject obj in objList)
        {
            pools[obj.name] = new Queue<GameObject>();

            GameObject parentPool = new GameObject($"{obj.name}");
            parentPool.transform.SetParent(this.transform);

            for (int i = 0; i < poolSize; i++)
            {
                GameObject go = Instantiate(obj, parentPool.transform);
                go.SetActive(false);
                pools[obj.name].Enqueue(go);
            }
        }
    }

    /// <summary>
    /// ObjectPoolManager에 붙어있는 부모(위치를 지정해주는) gameObject를 삭제하는 기능
    /// GameScene 재로드 시 하이라키에 pools가 남기 때문에 이를 삭제하는 용도
    /// </summary>
    private void ClearPool()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        pools.Clear();
    }

    public GameObject GetObject(string name)
    {
        if (!pools.ContainsKey(name))
            return null;

        if (pools[name].Count > 0)
        {
            GameObject go = pools[name].Dequeue();
            go.SetActive(true);
            return go;
        }
        else
        {
            GameObject go = Instantiate(objList.Find(obj => obj.name == name), transform.Find(name));
            go.SetActive(true);
            return go;
        }
    }

    public void ReturnObject(string name, GameObject go)
    {
        if (!pools.ContainsKey(name))
        {
            Destroy(go);
            return;
        }

        go.SetActive(false);
        pools[name].Enqueue(go);
    }

    //private void OnDestroy()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}
}