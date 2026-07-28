using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    #region varialbe
    private bool isClear;
    private bool isRetry = false;
    private bool isDespawning = false;
    private RoomLayoutData roomLayoutData;
    private RoomEntityData roomEntity;
    private List<GameObject> entityList;
    private List<GameObject> doors;
    private int aliveMonsterCnt;
    public RoomType roomType;
    public Dictionary<DirectionsEnum, Room> connectRoom;
    private DoorData doorData;
    #endregion

    private void Awake()
    {
        doorData = DataManager.Instance.DoorData;
        doorData.Init();
        entityList = new List<GameObject>();
        connectRoom = new Dictionary<DirectionsEnum, Room>();

        roomLayoutData = DataManager.Instance.RoomLayoutData;
        roomLayoutData.Init();
    }
    public Vector3 TelePort(DirectionsEnum dir)
    {
        return doorData.teleportPosDic[dir].position;
    }

    public Vector3 ReturnPos()
    {
        return new Vector3(transform.position.x + 8.5f, transform.position.y + 5.5f, 0f);
    }

    /// <summary>
    /// 현재 방 타입 지정
    /// RoomFactory에서 참조
    /// </summary>
    /// <param name="type"></param>
    public void SetRoom(RoomType type)
    {
        roomType = type;

        int roomCount = roomLayoutData.RoomDic[roomType].Count;

        if (roomCount > 0)
        {
            int idx = Random.Range(0, roomCount);

            roomEntity = roomLayoutData.RoomDic[roomType][idx].Clone();
            roomEntity.SetLocalToWroldRoomPostion(transform.position);
        }

        if (roomType == RoomType.StartRoom)
        {
            //RoomManager.Instance.ChangeRoom(this, null);
            IsaacManager.Instance.Player.transform.position = ReturnPos();
        }
        SetEntites();
    }

    /// <summary>
    /// 현재 방에 지정된 방 Entity를 소환하는 메서드
    /// -> Isaac이 방에 입장 시 호출
    /// </summary>
    private void SetEntites()
    {
        aliveMonsterCnt = 0;
        if (roomEntity != null)
        {
            entityList = SpawnManager.Instance.SpawnAll(roomEntity, (info) => true);
            CountMonsterEntity();
        }
        else
        {
            Debug.LogError("roomEntity null Error");
        }

        OnActiveEntity(false);
    }

    private void CountMonsterEntity()
    {
        foreach (var mon in roomEntity.spawnInfos)
        {
            if (mon.entityType == EntityType.Monster)
            {
                aliveMonsterCnt += 1;
                continue;
            }
        }
        isClear = CheckMonster();
    }

    private void OnActiveEntity(bool isBoolean)
    {
        foreach (var entity in entityList)
        {
            entity.SetActive(isBoolean);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Isaac"))
        {
            //RoomManager.Instance.ChangeRoom(this);
            OnPlayerEnterRoom();
        }
    }

    private void OnPlayerEnterRoom()
    {
        if (!isClear && roomEntity != null && isRetry)
        {
            entityList.AddRange(SpawnManager.Instance.SpawnAll(roomEntity, (info) => info.entityType == EntityType.Monster));
            aliveMonsterCnt = 0;
            CountMonsterEntity();
        }

        OnActiveEntity(true);
    }


    public void OnPlayerExitRoom()
    {
        if (!isClear)
        {
            isDespawning = true;

            SpawnManager.Instance.DeSpawn(entityList, (obj) => obj.layer == LayerMask.NameToLayer("Monster"));
            isRetry = true;

            isDespawning = false;
        }
        OnActiveEntity(false);
    }

    /// <summary>
    /// 해당 방 Monster Entity가  사망 시 cnt 지정
    /// -> 현재는 싱글톤 참조로  하고 있지만 이벤트 형식으로 변경해야함
    /// </summary>
    public void OnObjectReturn(GameObject obj)
    {
        entityList.Remove(obj);

        if (isDespawning)
            return;

        if (obj.TryGetComponent<MonsterController>(out var mon) && !isClear)
        {
            aliveMonsterCnt--;
        }

        isClear = CheckMonster();
        if (isClear)
        {
            Debug.Log("방 클리어. 몬스터 제거");
        }
    }

    private bool CheckMonster()
    {
        return aliveMonsterCnt <= 0 ? true : false;
    }

    public void DoorInstall()
    {
        foreach (var a in connectRoom)
        {
            doors.Add(SpawnManager.Instance.SpawnDoor(a.Key, this, a.Value));
        }
    }
}
