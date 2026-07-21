using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Monster, Item
}
public class EntitySpawnManager : Singleton<EntitySpawnManager>
{
    private Dictionary<EntityType, EntityFactory> factories;

    private void Awake()
    {
        factories = new Dictionary<EntityType, EntityFactory>()
        {
            { EntityType.Monster, new MonsterFactory() }
        };
    }
    public void Spawn(RoomEntityData data)
    {
        foreach (var a in data.spawnInfos)
        {
            if (factories.TryGetValue(a.entityType, out var factory))
            {
                factory.OnSpawnEntity(a);
            }
        }
    }
}

