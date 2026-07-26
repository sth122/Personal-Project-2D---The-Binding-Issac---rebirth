using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomLayout", menuName = "Data/RoomLayOut")]
public class RoomLayoutData : ScriptableObject
{
    /*
     Entity 정보 데이터
    Normal Room을 소환한다고 가정할 때 담겨 있어야 하는 내용
    1. 몬스터
    2. 아이템(확정 또는 확률)
    3. 지형 오브젝트

    RoomType(enum)
    ㄴ Normal
      ㄴRoomNumber
      ㄴ0
         ㄴEntity
          ㄴMonster - List
          ㄴItem - List
          ㄴObject - List
      ㄴ1
        ....
    ㄴ Boss
      ㄴ RoomNumber
        ....
     */

    public List<RoomEntityData> startRoom = new();
    public List<RoomEntityData> normalRoom = new();
    public List<RoomEntityData> bossRoom = new();
    public List<RoomEntityData> treasureRoom = new();

    // 나중에는   public Dictionary<StageType, Dictionary<RoomType, List<RoomEntityData>>> StageDic = new();
    public Dictionary<RoomType, List<RoomEntityData>> RoomDic = new();

    public void Init()
    {
        RoomDic[RoomType.Start] = startRoom;
        RoomDic[RoomType.Normal] = normalRoom;
        RoomDic[RoomType.Boss] = bossRoom;
        RoomDic[RoomType.Treasure] = treasureRoom;
    }
}

[Serializable]
public class RoomEntityData
{
    public List<SpawnInfo> spawnInfos;
    public RoomEntityData Clone()
    {
        List<SpawnInfo> cloneInfos = new();
        foreach (var info in spawnInfos)
        {
            cloneInfos.Add(info.Clone());
        }
        return new RoomEntityData(cloneInfos);
    }

    public RoomEntityData(List<SpawnInfo> spawnInfos)
    {
        this.spawnInfos = spawnInfos;
    }

    public void SetLocalToWroldRoomPostionn(Vector3 roomPos)
    {
        if (roomPos == null)
            Debug.LogError("currentRoomPos null");

        foreach (var spawnInfo in spawnInfos)
        {
            spawnInfo.SetSpawnPosition(roomPos);
        }
    }
}