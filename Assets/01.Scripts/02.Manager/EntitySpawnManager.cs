using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Monster, Item
}


abstract public class EntityFactory
{
    public abstract void OnSpawnEntity(SpawnInfo info);
}

public class MonsterFactory : EntityFactory
{
    public MonsterData monsterData = new MonsterData();
    private GameObject player = IsaacManager.Instance.Player;

    public override void OnSpawnEntity(SpawnInfo info)
    {
        MonsterInfo mStat = monsterData.monsterList[info.ID];
        var monster = ObjectPoolManager.Instance.GetObject(mStat.Clone().name);
        monster.transform.position = info.position;

        if (monster != null)
        {
            MonsterController mController = monster.GetComponent<MonsterController>();
            mController.InitData(mStat, player.transform);
            mController.Appear();
        }
    }
}



public class EntitySpawnManager : Singleton<EntitySpawnManager>
{
    private MonsterFactory monsterFactory;
    private Dictionary<EntityType, EntityFactory> factories;

    private void Awake()
    {
        factories = new Dictionary<EntityType, EntityFactory>()
        {
            {EntityType.Monster, monsterFactory }
        };
    }
    public void Spawn(RoomEntityData data)
    {
        foreach(var a in data.spawnInfos)
        {
            if (factories.TryGetValue(a.entityType, out var factory))
            {
                factory.OnSpawnEntity(a);
            }
        }
    }
}
