using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    private bool isClear;
    private RoomLayoutData roomLayoutData;
    private RoomEntityData roomEntity;
    private RoomType roomType;

    private void Start()
    {
        roomLayoutData = DataManager.Instance.RoomLayoutData;
    }

    public void Init()
    {
        isClear = false;
        
    }

    public void SetRoom(RoomType type)
    {
        this.roomType = type;

        int roomCount = roomLayoutData.RoomDic[roomType].Count;

        if(roomCount > 0 )
        {
            int idx = Random.Range(0, roomCount);

            roomEntity = roomLayoutData.RoomDic[roomType][idx].Clone();
            roomEntity.SetLocalToWroldRoomPostion(transform.position);
        }
    }

    private void SpawnEntites()
    {
        if(roomEntity != null)
        {
            SpawnManager.Instance.SpawnAll(roomEntity);
        }
        else
        {
            Debug.LogError("roomEntity null Error");
        }
    }


}
