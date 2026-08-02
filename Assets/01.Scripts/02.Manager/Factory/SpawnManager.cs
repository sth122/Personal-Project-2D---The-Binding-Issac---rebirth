using System;
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
    private DoorFactory doorFactory;
    private SpawnInfo cloneInfo;

    protected override void Awake()
    {
        isDDOL = false;
        base.Awake();

        factoryMap = new Dictionary<EntityType, EntityFactory>()
        {
            { EntityType.Monster, new MonsterFactory() },
            { EntityType.Item, new ItemFactory() },
            { EntityType.Obstacle, new ObstacleFactory() }
        };
        bulletFactory = new BulletFactory();
        roomFactory = new RoomFactory();
        doorFactory = new DoorFactory();
    }

    public List<GameObject> SpawnAll(RoomEntityData data, Func<SpawnInfo, bool> isSpawn)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (var k in data.spawnInfos)
        {
            if (!isSpawn(k))
                continue;

            cloneInfo = k.Clone();
            if (factoryMap.ContainsKey(k.entityType))
            {
                list.Add(factoryMap[k.entityType].OnSpawnEntity(cloneInfo));
            }
        }
        return list;
    }

    public void DeSpawn(List<GameObject> obj, Func<GameObject, bool> isObj)
    {
        List<GameObject> copy = new List<GameObject>(obj);

        foreach(var k in copy)
        {
            if(!isObj(k))
                continue;

            if(k.TryGetComponent<IReturnPool>(out var r))
            {
                r.ReturnPool();
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

    public GameObject SpawnDoor(DirectionsEnum vec, Room currentRoom, Room nextRoom)
    {
        return doorFactory.OnSpawnDoor(vec, currentRoom, nextRoom);
    }
}