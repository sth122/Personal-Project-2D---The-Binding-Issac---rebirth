using UnityEngine;

public class Room : MonoBehaviour
{
    private bool isClear;
    private RoomLayoutData roomLayoutData;
    private RoomEntityData roomEntity;
    private int aliveMonsterCnt;
    private RoomType roomType;

    private void Awake()
    {
        roomLayoutData = DataManager.Instance.RoomLayoutData;
        roomLayoutData.Init();
    }

    public void Init()
    {
        isClear = false;
        
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
        aliveMonsterCnt = 0;

        if(roomCount > 0 )
        {
            int idx = Random.Range(0, roomCount);

            roomEntity = roomLayoutData.RoomDic[roomType][idx].Clone();
            roomEntity.SetLocalToWroldRoomPostion(transform.position);
        }

        if(roomType == RoomType.StartRoom)
        {
            RoomManager.Instance.SetCurrentRoom(this);
        }
    }

    /// <summary>
    /// 현재 방에 지정된 방 Entity를 소환하는 메서드
    /// -> Isaac이 방에 입장 시 호출
    /// </summary>
    private void SpawnEntites()
    {
        if(roomEntity != null)
        {
            SpawnManager.Instance.SpawnAll(roomEntity);
            foreach(var mon in roomEntity.spawnInfos)
            {
                if(mon.entityType == EntityType.Monster)
                {
                    aliveMonsterCnt++;
                    continue;
                }
            }
        }
        else
        {
            Debug.LogError("roomEntity null Error");
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
        RoomManager.Instance.SetCurrentRoom(this);
        if(!isClear && roomEntity != null)
        {
            SpawnEntites();
        }
    }

    /// <summary>
    /// 해당 방 Monster Entity가  사망 시 cnt 지정
    /// -> 현재는 싱글톤 참조로  하고 있지만 이벤트 형식으로 변경해야함
    /// </summary>
    public void OnMonsterDied()
    {
        aliveMonsterCnt--;

        if(aliveMonsterCnt <= 0)
        {
            isClear = true;
            // 방 클리어 시 monster entity 삭제
            roomEntity.spawnInfos.RemoveAll(info => info.entityType == EntityType.Monster);
            Debug.Log("방 클리어. 몬스터 제거");
        }
    }


}
