//using UnityEngine;

//public class BulletPoolManager : ObjectPoolManager<PooledObject<IsaacBullet>>
//{
//    protected override void Initialize()
//    {
//        base.Initialize();
//        InitPool();
//    }

//    protected override PooledObject<IsaacBullet> CreatePooledObject(PooledObject<IsaacBullet> prefab, string poolName)
//    {

//        PooledObject<IsaacBullet> obj = Instantiate(prefab);
//        obj.transform.SetParent(transform, false);
//        obj.gameObject.SetActive(false);
//        obj.Initialize(this, poolName);

//        return obj;
//    }
//}
