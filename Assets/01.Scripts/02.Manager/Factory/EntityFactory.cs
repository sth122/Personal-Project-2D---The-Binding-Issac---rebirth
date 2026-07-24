using UnityEngine;

abstract public class EntityFactory
{
    public abstract void OnSpawnEntity(SpawnInfo info);
}


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

public class ItemFactory : EntityFactory
{
    private ItemData itemData;
    public ItemFactory()
    {
        itemData = DataManager.Instance.ItemData;
    }

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