using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Monster, Item, Obstacle
}


public class SpawnManager : Singleton<SpawnManager>
{
    private Dictionary<EntityType, EntityFactory> factoryMap;
    private BulletFactory bulletFactory;
    private RoomFactory roomFactory;
    private SpawnInfo cloneInfo;

    protected override void Initialize()
    {
        factoryMap = new Dictionary<EntityType, EntityFactory>()
        {
            { EntityType.Monster, new MonsterFactory() },
            { EntityType.Item, new ItemFactory() },
            { EntityType.Obstacle, new ObstacleFactory() }
        };
        bulletFactory = new BulletFactory();
        roomFactory = new RoomFactory();
    }

    public void SpawnAll(RoomEntityData data)
    {
        foreach (var k in data.spawnInfos)
        {
            cloneInfo = k.Clone();
            if (factoryMap.ContainsKey(k.entityType))
            {
                factoryMap[k.entityType].OnSpawnEntity(cloneInfo);
            }
        }
    }

    public GameObject SpawnBullet(TearType type)
    {
        return bulletFactory.OnSpawnBullet(type);
    }

    public GameObject SpawnRoom(Vector2Int coordinate, RoomType type)
    {
        return roomFactory.OnSpawnRoom(coordinate, type);
    }
}