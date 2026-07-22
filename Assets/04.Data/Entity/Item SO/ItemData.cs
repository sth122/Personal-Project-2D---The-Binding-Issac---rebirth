using System.Collections.Generic;
using UnityEngine;

public struct ItemInfo
{
    public int id;
    public string name;
    EntityType type;
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public List<ItemInfo> itemList = new List<ItemInfo>();
}
