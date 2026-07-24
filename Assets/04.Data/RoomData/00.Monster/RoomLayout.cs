
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SpawnInfo
{
    public int monsterID;
    public Vector3 position;
}

[CreateAssetMenu(fileName = "RoomLayout", menuName = "Data/RoomLayOut")]
public class RoomLayoutData : ScriptableObject
{
    public List<SpawnInfo> spawnList = new List<SpawnInfo>();
}
