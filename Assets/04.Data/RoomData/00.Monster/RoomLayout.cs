
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SpawnInfo
{
    public EntityType entityType;
    public int ID;
    public Vector3 position;
}

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

    public List<RoomData> roomDatas = new();
}

[Serializable]
public class RoomEntityData
{
    public List<SpawnInfo> spawnInfos;
}

[Serializable]
public class RoomData
{
    public RoomType roomType;
    public List<RoomEntityData> entities = new List<RoomEntityData>();
}