using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RoomType
{
    StartRoom, NormalRoom, BossRoom, TreasureRoom, ShopRoom, SecretRoom, DevilRoom, AngelRoom
}

public class RoomGenerator
{
    #region variable
    private Stack<Vector2Int> roomStack;
    private Dictionary<Vector2Int, int> endRoomList;
    public Dictionary<Vector2Int, RoomType> roomMap;

    private int gridSize = 11;
    private int maxRoomsCount;
    private int roomsGenerated;

    private int[,] map;
    #endregion

    private List<Vector2Int> direction = new List<Vector2Int>
    {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };

    public RoomGenerator()
    {
        roomStack = new Stack<Vector2Int>();
        endRoomList = new Dictionary<Vector2Int, int>();
        roomMap = new Dictionary<Vector2Int, RoomType>();
    }

    /// <summary>
    /// Map 11 x 11 Grid 생성
    /// </summary>
    public IEnumerator GenerateMapGrid()
    {
        // 최대 방의 개수 = (1 or 2) + 5 + stagelevel * 2
        maxRoomsCount = Random.Range(1, 3) + 5 + StageManager.Instance.stageCnt * 2;

        bool success = false;


        // 방 생성 실패 시 재생성
        while(!success)
        {
            InitMap();

            GenerateRooms();

            if(roomsGenerated >= maxRoomsCount)
            {
                success = true;
            }
            else
            {
                yield return null;
            }
        }

        FindEndRoom();

        Debug.Log($"끝 방 개수 {endRoomList.Count}");
        
        AssignBossRoom();

        AssignTreasureRoom();
    }

    #region NormalRoom Generator
    /// <summary>
    /// 맵 초기화
    /// </summary>
    private void InitMap()
    {
        // 전체 방 그리드 11 x 11
        map = new int[gridSize, gridSize];

        roomMap.Clear();
        roomStack.Clear();
        endRoomList.Clear();

        Vector2Int start = new Vector2Int(gridSize / 2, gridSize / 2);

        map[start.x, start.y] = 1;
        roomMap[start] = RoomType.StartRoom;

        roomStack.Push(start);
        roomsGenerated = 1;

        Debug.Log($"시작 지점 생성 [{start.x}, {start.y}]");
    }

    /// <summary>
    /// DFS를 이용한 룸 배치 배열 탐색
    /// </summary>
    private void GenerateRooms()
    {
        // Stack
        while (roomStack.Count > 0)
        {
            Vector2Int currentRoom = roomStack.Peek();
            bool isCreate = false;

            List<Vector2Int> shuffleDirs = GetRandomDirection();

            foreach (Vector2Int dir in shuffleDirs)
            {
                Vector2Int nextRoom = currentRoom + dir;

                if (!TryCreateRoom(nextRoom))
                    continue;

                CreateRoom(nextRoom);
                Debug.Log($"방 생성 [{nextRoom.x}, {nextRoom.y}]");
                roomStack.Push(nextRoom);
                isCreate = true;
                break;
            }

            if (!isCreate)
            {
                // 다음 방 생성 실패 시 Pop
                roomStack.Pop();
            }
        }
    }

    /// <summary>
    /// Fisher-Yates Shuffle 방을 생성하는 방향 매번 랜덤하게
    /// </summary>
    /// <returns></returns>
    private List<Vector2Int> GetRandomDirection()
    {
        List<Vector2Int> dirs = new List<Vector2Int>(direction);

        for (int i = 0; i < dirs.Count; i++)
        {
            int rand = Random.Range(i, dirs.Count);

            (dirs[i], dirs[rand]) = (dirs[rand], dirs[i]);
        }

        return dirs;
    }

    /// <summary>
    /// 생성된 방 맵 그리드에 삽입
    /// </summary>
    /// <param name="roomPos"></param>
    private void CreateRoom(Vector2Int roomPos)
    {
        map[roomPos.x, roomPos.y] = 1;

        roomMap[roomPos] = RoomType.NormalRoom;

        roomsGenerated++;
    }

    /// <summary>
    /// 맵 생성 조건
    /// 1. 이미 방이 생성된 경우 포기
    /// 2. 만들고자 하는 방에 이미 인접한 방이 2개 이상 있을 경우 포기
    /// 3. 스테이지에 생성될 방의 목표치를 채웠을 경우 포기
    /// 4. 50% 확률로 코인 던지기를 통해 실패 시 포기
    /// </summary>
    /// <param name="_currentRoom"></param>
    /// <param name="_roomsGenerated"></param>
    /// <returns></returns>
    private bool TryCreateRoom(Vector2Int _targetRoom)
    {
        // grid 배열 체크
        if (_targetRoom.x < 0 || _targetRoom.x >= gridSize || _targetRoom.y < 0 || _targetRoom.y >= gridSize)
            return false;

        if (map[_targetRoom.x, _targetRoom.y] == 1)
            return false;

        if (CheckAdjacentRoom(_targetRoom) >= 2)
            return false;

        if (Random.Range(0, 2) == 0)
            return false;


        return true;
    }

    /// <summary>
    /// 인접한 방이 2개 이상 있는지 체크
    /// </summary>
    /// <param name="_targetRoom"></param>
    /// <returns></returns>
    private int CheckAdjacentRoom(Vector2Int _targetRoom)
    {
        int cnt = 0;
        Vector2Int checkPos;
        foreach (Vector2Int dir in direction)
        {
            checkPos = _targetRoom + dir;
            if (checkPos.x >= 0 && checkPos.y >= 0 && checkPos.x < gridSize
                && checkPos.y < gridSize && map[checkPos.x, checkPos.y] == 1)
            {
                cnt++;
            }
        }
        return cnt;
    }
    #endregion


    #region SpecialRoom Generator
    private void FindEndRoom()
    {
        // 혹시 모를 다시 한 번 더 초기화
        endRoomList.Clear();

        foreach (var room in roomMap)
        {
            if (room.Value != RoomType.NormalRoom)
                continue;

            int adjacentRoom = CheckAdjacentRoom(room.Key);

            if (adjacentRoom == 1)
            {
                int center = gridSize / 5;
                int distance = Mathf.Abs(room.Key.x - center) +
                               Mathf.Abs(room.Key.y - center);
                endRoomList.Add(room.Key, distance);
            }
        }
    }
    private void AssignBossRoom()
    {
        if (endRoomList.Count == 0)
        {
            Debug.LogWarning("보스 방 부족");
            return;
        }

        Vector2Int boss = Vector2Int.zero;
        int maxDistance = -1;

        foreach (var room in endRoomList)
        {
            if (room.Value > maxDistance)
            {
                maxDistance = room.Value;
                boss = room.Key;
            }
        }
        Debug.Log($"보스 방 생성 [{boss.x}, {boss.y}]");
        endRoomList.Remove(boss);
        roomMap[boss] = RoomType.BossRoom;
    }
    private void AssignTreasureRoom()
    {
        if (endRoomList.Count == 0)
        {
            Debug.LogWarning("endRoom 부족");
            return;
        }

        Vector2Int treasure = Vector2Int.zero;
        int maxDistance = -1;

        foreach (var room in endRoomList)
        {
            if (room.Value > maxDistance)
            {
                maxDistance = room.Value;
                treasure = room.Key;
            }
        }
        Debug.Log($"보물 방 생성 [{treasure.x}, {treasure.y}]");
        endRoomList.Remove(treasure);
        roomMap[treasure] = RoomType.TreasureRoom;
    }
    #endregion
}