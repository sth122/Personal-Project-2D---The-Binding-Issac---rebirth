using System.Collections.Generic;
using UnityEngine;

public class DoorFactory
{
    private Dictionary<RoomType, string> doors = new Dictionary<RoomType, string>();
    private DoorData doorData;
    //private bool isInit = false;


    public DoorFactory()
    {
        doors[RoomType.StartRoom] = DoorType.NormalDoor.ToString();
        doors[RoomType.NormalRoom] = DoorType.NormalDoor.ToString();
        doors[RoomType.BossRoom] = DoorType.BossDoor.ToString();
        doors[RoomType.TreasureRoom] = DoorType.TreasureDoor.ToString();
        doorData = DataManager.Instance.DoorData;
        doorData.Init();
    }


    // 방향과 문 타입을 지정
    public GameObject OnSpawnDoor(DirectionsEnum vec, Room currentRoom, Room nextRoom)
    {
        if(nextRoom  == null)
        {
            Debug.Log("다음 방 없음");
            return null;
        }

        if (doors.ContainsKey(nextRoom.roomType))
        {
            if (nextRoom.roomType != RoomType.NormalRoom)
            {
                return SelectRoomType(vec, nextRoom.roomType, currentRoom, nextRoom);
            }
            else
            {
                return SelectRoomType(vec, currentRoom.roomType, currentRoom, nextRoom);
            }
        }
        else
        {
            Debug.LogError("Door null");
            return null;
        }
    }

    private GameObject SelectRoomType(DirectionsEnum vec, RoomType type, Room currentRoom, Room nextRoom)
    {
        GameObject doorObj = ObjectPoolManager.Instance.GetObject(doors[type]);
        if (doorObj != null)
        {
            Door door = doorObj.GetComponent<Door>();
            door.SetRoom(currentRoom, nextRoom);
            door.SetTransform(doorData.doorPosDic[vec]);
        }
        return doorObj;
    }
}
// 1. 본인의 RoomType이 normal이 아니라면  해당 방의 모든 문은 RoomType에 맞는 door가 된다.
// 2. Normal의 방은 nextRoom이 RoomType normal이 아니면 nextRoom의 RoomType에 맞는 door가 된다.