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
        var monster = ObjectPoolManager.Instance.GetObject(mStat.Clone().name);
        monster.transform.position = info.spawnPos;

        if (monster != null)
        {
            MonsterController mController = monster.GetComponent<MonsterController>();
            mController.InitData(mStat, player.transform);
            mController.Appear();
        }
    }
}