using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorFactory
{
    private Dictionary<RoomType, string> doors = new Dictionary<RoomType, string>();
    public DoorData doorData;

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
            GameObject doorObj = ObjectPoolManager.Instance.GetObject(doors[nextRoom.roomType]);
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
