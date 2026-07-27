using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] public GameObject Player;
    private RoomGenerator roomGenerator;
    private Dictionary<Vector2Int, Room> spawnRoomMap;
    public Room currentRoom;

    private Vector2Int[] directions = new Vector2Int[]
    {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };

    protected override void Awake()
    {
        base.Awake();
        roomGenerator = new RoomGenerator();
        spawnRoomMap = new Dictionary<Vector2Int, Room>();
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

        Debug.Log("방 소환 시작");
        foreach(var room in roomGenerator.roomMap)
        {
            GameObject roomObj = SpawnManager.Instance.SpawnRoom(room.Key.coordinate, room.Value);
            if(roomObj != null && roomObj.TryGetComponent<Room>(out var roomComp))
            {
                spawnRoomMap[room.Key.coordinate] = roomComp;
            }
        }

        DoorInstallInTheRoom();
    }



    public void ChangeRoom(Room room)
    {
        if(currentRoom != null)
        {
            currentRoom.OnPlayerExitRoom();
        }
        currentRoom = room;
        CameraRoomRock.Instance.SetCameraPosition(room.transform);
    }
    
    private void DoorInstallInTheRoom()
    {
        List<Vector2Int> doorCoordinate = new List<Vector2Int>();
        
        foreach(var vec2 in spawnRoomMap)
        {
            foreach(Vector2Int v in directions)
            {
                Vector2Int checkPos = vec2.Key + v;
                if(spawnRoomMap.ContainsKey(checkPos))
                {
                    doorCoordinate.Add(checkPos);
                }
            }

            doorCoordinate.Clear();
        }
    }

}
