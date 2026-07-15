using UnityEngine;

public class MonsterPoolManager : ObjectPoolManager<PooledObject<MonsterController>>
{
    protected override void Initialize()
    {
        base.Initialize();
        InitPool();
    }

    protected override PooledObject<MonsterController> CreatePooledObject(PooledObject<MonsterController> prefab, string poolName)
    {
        PooledObject<MonsterController> obj = Instantiate(prefab);
        obj.transform.SetParent(transform, false);
        obj.gameObject.SetActive(false);
        obj.Initialize(this, poolName);

        return obj;
    }
}
