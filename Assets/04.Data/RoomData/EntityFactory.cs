using System;
using UnityEngine;

abstract public class EntitySpawn
{
    protected SpawnInfo info;
    public abstract void OnSpawn();

    public void GetInfo(SpawnInfo info)
    {
        this.info = info;
    }
}

[Serializable]
public class MonsterSpawn : EntitySpawn
{
    public MonsterData monsterData;

    public override void OnSpawn()
    {
        //MonsterInfo mStat = monsterData.monsterList.Find(x => x.monsterID == ID);
        //var monster = ObjectPoolManager.Instance.GetObject(mStat.Clone().name);
        //monster.transform.position = position;

        //if (monster != null)
        //{
        //    MonsterController mController = monster.GetComponent<MonsterController>();
        //    //mController.InitData(mStat, Player.transform);
        //    mController.Appear();
        //}
    }
}



public enum EntityType
{
    Monster, Item
}

abstract public class EntityFactory : Singleton<EntityFactory>
{
    public abstract EntitySpawn OnSpawnEntity(EntityType type);
}

public class MonsterFactory : EntityFactory
{
    public override EntitySpawn OnSpawnEntity(EntityType type)
    {
        MonsterSpawn info = null;

        switch (type)
        {
            case EntityType.Monster:
                info = new MonsterSpawn();
                break;
        }

        return info;
    }

    //public abstract void OnSpawn(EntityType type);
}

public class FactoryUse : MonoBehaviour
{
    private void Start()
    {
        EntitySpawn info = EntityFactory.Instance.OnSpawnEntity(EntityType.Monster);
        info.OnSpawn();
    }
}
