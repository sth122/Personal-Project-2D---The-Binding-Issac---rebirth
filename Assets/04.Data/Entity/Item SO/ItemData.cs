using System;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    PickUp, Passive, Active, Accessories
}
public enum PickUpType
{
    Heart, Coin, Bomb, Key, Pill, Card
}

[Serializable]
public struct ItemInfo
{
    public int id;
    public string name;
    public EntityType type;
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public List<ItemInfo> heartItemList = new List<ItemInfo>();
    public List<ItemInfo> coinItemList = new List<ItemInfo>();
    public List<ItemInfo> bombItemList = new List<ItemInfo>();
    public List<ItemInfo> keyItemList = new List<ItemInfo>();
    public List<ItemInfo> pillItemList = new List<ItemInfo>();
    public List<ItemInfo> cardItemList = new List<ItemInfo>();

}
