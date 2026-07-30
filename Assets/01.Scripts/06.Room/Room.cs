using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    #region varialbe
    private bool isClear;
    private bool isRetry = false;
    private bool isDespawning = false;
    private bool isInit = false;
    private RoomLayoutData roomLayoutData;
    private RoomEntityData roomEntity;
    private List<GameObject> entityList;
    private List<GameObject> doors;
    private int aliveMonsterCnt;
    public RoomType roomType;
    public Dictionary<DirectionsEnum, Room> connectRoom;
    private DoorData doorData;

    public Tilemap wallTilemap;
    public TileBase wall;
    #endregion

    private void Awake()
    {
        doors = new List<GameObject>();

        entityList = new List<GameObject>();
        connectRoom = new Dictionary<DirectionsEnum, Room>();
    }

    private void Init()
    {
        if (isInit) return;

        doorData = DataManager.Instance.DoorData;
        roomLayoutData = DataManager.Instance.RoomLayoutData;

        doorData.Init();
        roomLayoutData.Init();

        isInit = true;
    }

    public Vector3 TelePort(DirectionsEnum dir)
    {
        return transform.position + doorData.teleportPosDic[dir].position;
    }

    /// <summary>
    /// 현재 방 타입 지정
    /// RoomFactory에서 참조
    /// </summary>
    /// <param name="type"></param>
    public void SetRoom(RoomType type)
    {
        if (!isInit)
            Init();

        roomType = type;

        int roomCount = roomLayoutData.RoomDic[roomType].Count;

        if (roomCount > 0)
        {
            int idx = UnityEngine.Random.Range(0, roomCount);

            roomEntity = roomLayoutData.RoomDic[roomType][idx].Clone();
            roomEntity.SetLocalToWroldRoomPostion(transform.position);
        }

        SetEntites();

        if (roomType == RoomType.StartRoom)
        {
            RoomManager.Instance.ChangeRoom(this, DirectionsEnum.Center);
        }
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

            DoorStateUpdate();
        }
    }

    private bool CheckMonster()
    {
        return aliveMonsterCnt <= 0 ? true : false;
    }

    public void DoorInstall()
    {
        foreach (DirectionsEnum dir in Enum.GetValues(typeof(DirectionsEnum)))
        {
            if (connectRoom.TryGetValue(dir, out Room nextRoom))
            {
                doors.Add(SpawnManager.Instance.SpawnDoor(dir, this, nextRoom));
                SetWallTilemap(dir, null);
            }
        }
    }

    private void SetWallTilemap(DirectionsEnum dir, TileBase tile)
    {
        if (dir == DirectionsEnum.Center || wallTilemap == null)
            return;

        Vector3 doorPos = transform.position + doorData.doorPosDic[dir].postion;
        Vector3Int wallPos = wallTilemap.WorldToCell(doorPos);

        wallTilemap.SetTile(wallPos, tile);

        switch (dir)
        {
            case DirectionsEnum.Down:
                wallTilemap.SetTile(wallPos + new Vector3Int(0, -1, 0), null);
                break;
            case DirectionsEnum.Left:
                wallTilemap.SetTile(wallPos + new Vector3Int(-1, 0, 0), null);
                break;
            default:
                break;
        }
    }

    public void DoorStateUpdate()
    {
        if(doors == null)
        {
            Debug.LogError("DoorStateUpdate doors null error");
        }

        foreach (var doorObj in doors)
        {
            if(doorObj.TryGetComponent<Door>(out Door door))
            {
                door.SetDoorState(isClear);
            }
        }
    }
}
