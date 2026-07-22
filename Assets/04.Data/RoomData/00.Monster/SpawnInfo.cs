using System;
using UnityEngine;

/// <summary>
/// 방에 소환되는 Entity 정보
/// 타입 / ID / 위치
/// </summary>
[Serializable]
public class SpawnInfo
{
    public EntityType entityType;
    public int id;
    public Vector3 position;
    public Vector3 spawnPos = Vector3.zero;

    public SpawnInfo(EntityType entityType, int id, Vector3 position, Vector3 spawnPos)
    {
        this.entityType = entityType;
        this.id = id;
        this.position = position;
        this.spawnPos = spawnPos;
    }

    public SpawnInfo Clone() { return new SpawnInfo(entityType, id, position, spawnPos); }

    public void SetSpawnPostion(Vector3 currentRoomPos)
    {
        if (currentRoomPos == null)
            Debug.LogError("currentRoomPos null");

        spawnPos = new Vector3(currentRoomPos.x + position.x + 2.5f, currentRoomPos.y + position.y + 2.5f, 0);
    }
}
