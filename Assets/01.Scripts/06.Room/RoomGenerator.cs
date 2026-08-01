using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RoomType
{
    StartRoom, NormalRoom, BossRoom, TreasureRoom, ShopRoom, SecretRoom, DevilRoom, AngelRoom, EscapeRoom
}

public struct RoomDepth
{
    public Vector2Int coordinate;
    public int depth;

    public RoomDepth(Vector2Int coordinate, int depth)
    {
        this.coordinate = coordinate;
        this.depth = depth;
    }
}

/// <summary>
/// 방 랜덤 생성 알고리즘 순수 클래스
/// </summary>
public class RoomGenerator
{
    #region variable
    private Stack<RoomDepth> roomStack;
    private Dictionary<RoomDepth, int> endRoomList;
    public Dictionary<RoomDepth, RoomType> roomMap;

    private int gridSize = 11;
    private int roomCount;
    private int roomsGenerated;

    private int[,] map;
    #endregion

    private List<Vector2Int> direction = new List<Vector2Int>
    {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };

    public RoomGenerator()
    {
        roomStack = new Stack<RoomDepth>();
        endRoomList = new Dictionary<RoomDepth, int>();
        roomMap = new Dictionary<RoomDepth, RoomType>();
    }

    /// <summary>
    /// Map 11 x 11 Grid 생성
    /// 방 생성 알고리즘
    /// </summary>
    public IEnumerator GenerateMapGrid()
    {
        // 방의 개수 = (1 or 2) + 5 + stagelevel * 2
        roomCount = Random.Range(1, 3) + 5 + StageManager.Instance.stageCnt * 2;
        bool success = false;
        bool subSuccess = false;
        
        while (!subSuccess)
        {
            // 방 생성 실패 시 재생성
            while (!success)
            {
                InitMap();

                GenerateRooms();

                if (roomsGenerated == roomCount)
                {
                    success = true;
                }
                else
                {
                    yield return null;
                }
            }
            FindEndRoom();

            // 끝 방 최소 3개
            if (endRoomList.Count >= 3)
            {
                subSuccess = true;
            }
            else
            {
                success = false;
                yield return null;
            }
        }

        yield return new WaitWhile(() => !subSuccess);

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

        Vector2Int startVec = new Vector2Int(gridSize / 2, gridSize / 2);

        RoomDepth start = new RoomDepth(startVec, 0);

        map[start.coordinate.x, start.coordinate.y] = 1;
        roomMap[start] = RoomType.StartRoom;

        roomStack.Push(start);
        roomsGenerated = 1;
    }

    /// <summary>
    /// DFS를 이용한 룸 배치 배열 탐색
    /// </summary>
    private void GenerateRooms()
    {
        while (roomStack.Count > 0 && roomsGenerated < roomCount)   // Stack
        {
            RoomDepth currentRoom = roomStack.Peek();
            bool isCreate = false;

            List<Vector2Int> shuffleDirs = GetRandomDirection();

            foreach (Vector2Int dir in shuffleDirs)
            {
                Vector2Int nextRoom = currentRoom.coordinate + dir;
                if (!TryCreateRoom(nextRoom))
                    continue;

                CreateRoom(nextRoom, currentRoom.depth + 1);
                isCreate = true;
                break;
            }

            if (!isCreate)
            {
                roomStack.Pop(); // 다음 방 생성 실패 시 Pop
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
    private void CreateRoom(Vector2Int roomPos, int increaseDepth)
    {
        map[roomPos.x, roomPos.y] = 1;
        ++roomsGenerated;

        RoomDepth nextRoom = new RoomDepth(roomPos, increaseDepth);

        roomMap[nextRoom] = RoomType.NormalRoom;
        roomStack.Push(nextRoom);
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
    /// <summary>
    /// 끝 방 찾기
    /// </summary>
    private void FindEndRoom()
    {
        // 혹시 모를 다시 한 번 더 초기화
        endRoomList.Clear();

        foreach (var room in roomMap)
        {
            if (room.Value != RoomType.NormalRoom)
                continue;

            int adjacentRoom = CheckAdjacentRoom(room.Key.coordinate);

            if (adjacentRoom == 1)
            {
                endRoomList.Add(room.Key, room.Key.depth);
            }
        }
    }

    /// <summary>
    /// 보스 방 선정
    /// </summary>
    private void AssignBossRoom()
    {
        if (endRoomList.Count == 0)
        {
            Debug.LogWarning("보스 방 부족");
            return;
        }

        RoomDepth boss = new RoomDepth(Vector2Int.zero, 0);
        int maxDistance = -1;

        foreach (var room in endRoomList)
        {
            if (room.Value > maxDistance)
            {
                maxDistance = room.Value;
                boss = room.Key;
            }
        }
        
        endRoomList.Remove(boss);
        roomMap[boss] = RoomType.BossRoom;
    }

    /// <summary>
    /// (특별 방) 보물 방 선정
    /// </summary>
    private void AssignTreasureRoom()
    {
        if (endRoomList.Count == 0)
        {
            Debug.LogWarning("endRoom 부족");
            return;
        }

        RoomDepth treasure = new RoomDepth(Vector2Int.zero, 0);
        int maxDistance = -1;

        foreach (var room in endRoomList)
        {
            if (room.Value > maxDistance)
            {
                maxDistance = room.Value;
                treasure = room.Key;
            }
        }
        endRoomList.Remove(treasure);
        roomMap[treasure] = RoomType.TreasureRoom;
    }
    #endregion
}