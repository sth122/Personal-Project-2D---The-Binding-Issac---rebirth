using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Normal, Boss, Treasure, Shop, Secret, Devil, Angel
}

public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] GameObject Player;
    public MonsterData monsterStatData;
    //public EntityData entityData;

    public RoomLayoutData roomLayoutData;
    private RoomType currentRoomType;

    public void GetRoomType(RoomType type)
    {
        this.currentRoomType = type;
    }

    // Room Script에서 소환하거나 StageManager에서 
    // 나중에 entityInfo 추상클래스로 만들어서 통합
    
    private void OnSpawnMonster() 
    {
        foreach (var spawnInfo in roomLayoutData.RoomList[currentRoomType])
        {
            MonsterInfo mStat = monsterStatData.monsterList.Find(x => x.monsterID == spawnInfo.ID);
            var monster = ObjectPoolManager.Instance.GetObject(mStat.Clone().name);
            monster.transform.position = spawnInfo.position;

            if (monster != null)
            {
                MonsterController mController = monster.GetComponent<MonsterController>();
                mController.InitData(mStat, Player.transform);
                mController.Appear();
            }
        }
    }
    private void OnSpawnItem() { }
    
}
