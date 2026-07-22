using System.Collections.Generic;
public enum EntityType
{
    Monster, Item
}
public class EntitySpawnManager : Singleton<EntitySpawnManager>
{
    private Dictionary<EntityType, EntityFactory> factoryMap;
    private SpawnInfo cloneInfo;

    protected override void Initialize()
    {
        factoryMap = new Dictionary<EntityType, EntityFactory>()
        {
            { EntityType.Monster, new MonsterFactory() }
        };
    }

    public void Spawn(RoomEntityData data)
    {

        foreach(var k in data.spawnInfos)
        {
            cloneInfo = k.Clone();
            if(factoryMap.ContainsKey(k.entityType))
            {
                factoryMap[k.entityType].OnSpawnEntity(cloneInfo);
            }
        }
    }
}