using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum DirectionsEnum
{
    Up, Down, Left, Right, Center
}

public class RoomManager : Singleton<RoomManager>
{
    private RoomGenerator roomGenerator;
    private Dictionary<Vector2Int, Room> spawnRoomMap;
    private List<Room> roomsList;
    public Room currentRoom;

    private Dictionary<DirectionsEnum, Vector2Int> dirs = new Dictionary<DirectionsEnum, Vector2Int>()
    {
        {DirectionsEnum.Up, Vector2Int.up},
        {DirectionsEnum.Down, Vector2Int.down},
        {DirectionsEnum.Left, Vector2Int.left},
        {DirectionsEnum.Right, Vector2Int.right},
    };

    protected override void Awake()
    {
        base.Awake();
        roomGenerator = new RoomGenerator();
        roomsList = new List<Room>();
        spawnRoomMap = new Dictionary<Vector2Int, Room>();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void StartRoomsSpawn()
    {
        spawnRoomMap.Clear();
        StartCoroutine(SpawnAllRooms());
    }

    private IEnumerator SpawnAllRooms()
    {
        yield return StartCoroutine(roomGenerator.GenerateMapGrid());
        // 초기 테스트  타입
        // 방 생성할 시 타입 정하게 해야함

        yield return StartCoroutine(SpawnRoom());

        ConnectRoom();
    }

    private IEnumerator SpawnRoom()
    {
        Debug.Log("방 소환 시작");
        foreach (var room in roomGenerator.roomMap)
        {
            GameObject roomObj = SpawnManager.Instance.SpawnRoom(room.Key.coordinate, room.Value);
            if (roomObj != null && roomObj.TryGetComponent<Room>(out var roomComp))
            {
                roomsList.Add(roomComp);
                spawnRoomMap[room.Key.coordinate] = roomComp;
            }
            yield return null;
        }
    }

    public void ChangeRoom(Room nextRoom, DirectionsEnum dir)
    {
        if (currentRoom != null)
        {
            currentRoom.OnPlayerExitRoom();
        }
        currentRoom = nextRoom;
        currentRoom.DoorStateUpdate();
        CameraRoomRock.Instance.SetCameraPosition(nextRoom.transform);
        IsaacManager.Instance.isaac.transform.position = nextRoom.TelePort(dir);
    }

    private void ConnectRoom()
    {
        foreach (var vec2 in spawnRoomMap)
        {
            foreach (var dir in dirs)
            {
                Vector2Int checkPos = vec2.Key + dir.Value;
                if (spawnRoomMap.ContainsKey(checkPos))
                {
                    vec2.Value.connectRoom[dir.Key] = spawnRoomMap[checkPos];
                }
            }
            vec2.Value.DoorInstall();
        }
    }
}
