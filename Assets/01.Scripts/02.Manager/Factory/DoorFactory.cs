using System.Collections.Generic;
using UnityEngine;

public class DoorFactory
{
    private Dictionary<RoomType, string> doors = new Dictionary<RoomType, string>();
    public DoorData doorData;
    public RoomType roomType;

    public DoorFactory()
    {
        doors[RoomType.NormalRoom] = DoorType.NormalDoor.ToString();
        doors[RoomType.BossRoom] = DoorType.BossDoor.ToString();
        doors[RoomType.TreasureRoom] = DoorType.TreasureDoor.ToString();
        doorData = DataManager.Instance.DoorData;
        doorData.Init();
    }

    // 방향과 문 타입을 지정
    public GameObject OnSpawnDoor(DirectionsEnum vec, Room currentRoom, Room nextRoom)
    {
        if (doors.ContainsKey(nextRoom.roomType))
        {
            roomType = nextRoom.roomType;
            if (roomType == RoomType.StartRoom)
                roomType = RoomType.NormalRoom;
            GameObject doorObj = ObjectPoolManager.Instance.GetObject(doors[roomType]);
            if (doorObj != null)
            {
                Door door = doorObj.GetComponent<Door>();
                door.SetRoom(currentRoom, nextRoom);
                door.SetTransform(doorData.doorPosDic[vec]);
            }
            return doorObj;
        }
        else
        {
            Debug.LogError("Door null");
            return null;
        }
    }
}
