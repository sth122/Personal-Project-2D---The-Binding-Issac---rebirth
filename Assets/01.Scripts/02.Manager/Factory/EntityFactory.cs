using UnityEngine;

abstract public class EntityFactory
{
    public abstract void OnSpawnEntity(SpawnInfo info);
}

/// <summary>
/// 
/// </summary>
public class MonsterFactory : EntityFactory
{
    private MonsterData monsterData;
    private GameObject player;
    public MonsterFactory()
    {
        monsterData = DataManager.Instance.MonsterData;
        player = RoomManager.Instance.Player;
    }

    public override void OnSpawnEntity(SpawnInfo info)
    {
        MonsterInfo mStat = monsterData.monsterList[info.id];
        GameObject monster = ObjectPoolManager.Instance.GetObject(mStat.Clone().name);
        monster.transform.position = info.position;

        if (monster != null)
        {
            MonsterController mController = monster.GetComponent<MonsterController>();
            mController.InitData(mStat, player.transform);
            mController.Appear();
        }
    }
}

/// <summary>
/// 
/// </summary>
public class ItemFactory : EntityFactory
{
    private ItemData itemData;
    public ItemFactory()
    {
        itemData = DataManager.Instance.ItemData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    public override void OnSpawnEntity(SpawnInfo info)
    {
        ItemInfo itemInfo = itemData.itemListDic[info.id];
        GameObject item = ObjectPoolManager.Instance.GetObject(itemInfo.Clone().name);
        item.transform.position = info.position;
        if(item == null)
        {
            Debug.Log($"{info.id} not found");
        }
    }
}

public class ObstacleFactory : EntityFactory
{
    private ObstacleData obstacleData;
    public ObstacleFactory()
    {
        obstacleData = DataManager.Instance.ObstacleData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    public override void OnSpawnEntity(SpawnInfo info)
    {
        ObstacleInfo obstacleInfo = obstacleData.obstacleList[info.id];
        GameObject obstacle = ObjectPoolManager.Instance.GetObject(obstacleInfo.Clone().name);
        obstacle.transform.position = info.position;
        if (obstacleInfo == null)
        {
            Debug.Log($"{info.id} not found");
        }
    }
}