using System;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    PickUp, Passive, Active, Accessories
}
public enum PickUpType
{
    Coin, Bomb, Key, Pill, Card
}

[Serializable]
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
