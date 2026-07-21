using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Normal, Boss, Treasure, Shop, Secret, Devil, Angel
}

public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] GameObject Player;
    public RoomLayoutData roomLayoutData;
    public MonsterData monsterData;

    private RoomType currentRoomType;

    private void Awake()
    {
        //roomLayoutData.Init();
    }

    public void GetRoomType(RoomType type)
    {
        this.currentRoomType = type;
    }

    // Room Script에서 소환하거나 StageManager에서 
    // 나중에 entityInfo 추상클래스로 만들어서 통합
    
    public void OnSpawnEntity()
    {
        //int randomRoomIndex = Random.Range(0, roomLayoutData.RoomDic[currentRoomType].Count);

        //var room = roomLayoutData.RoomDic[currentRoomType][randomRoomIndex];

        //foreach(var entity in room.Values)
        //{
            
        //}


        //foreach (var entity in room.Values)
        //{
        //    if (entity == null)
        //    {
        //        Debug.LogError("RoomDic error");
                
        //    }
        //    MonsterInfo mStat = monsterData.monsterList.Find(x => x.monsterID == entity);
        //    var monster = ObjectPoolManager.Instance.GetObject(mStat.Clone().name);
        //    monster.transform.position = spawnInfo.position;

        //    if (monster != null)
        //    {
        //        MonsterController mController = monster.GetComponent<MonsterController>();
        //        mController.InitData(mStat, Player.transform);
        //        mController.Appear();
        //    }
        //}
    }

    private void OnSpawnMonster() 
    {
        //foreach (var spawnInfo in roomLayoutData.RoomList[currentRoomType])
        //{
        //    MonsterInfo mStat = monsterStatData.monsterList.Find(x => x.monsterID == spawnInfo.ID);
        //    var monster = ObjectPoolManager.Instance.GetObject(mStat.Clone().name);
        //    monster.transform.position = spawnInfo.position;

        //    if (monster != null)
        //    {
        //        MonsterController mController = monster.GetComponent<MonsterController>();
        //        mController.InitData(mStat, Player.transform);
        //        mController.Appear();
        //    }
        //}
    }
    private void OnSpawnItem() { }
    
}
