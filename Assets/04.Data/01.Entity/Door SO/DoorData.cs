using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct DoorInfo
{
    public DoorType doorType;

    public DoorInfo(DoorType doorType)
    {
        this.doorType = doorType;
    }
}

[CreateAssetMenu(fileName = "DoorData", menuName = "Data/DoorData")]
public class DoorData : ScriptableObject
{
    public List<DoorInfo> doors = new List<DoorInfo>();
}
