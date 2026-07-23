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
public class ItemInfo
{
    public int id;
    public string name;
    public PickUpType type;
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public List<ItemInfo> pickUpItemList = new List<ItemInfo>();
    public List<ItemInfo> passiveItemList = new List<ItemInfo>();
    public List<ItemInfo> activeItemList = new List<ItemInfo>();
    public List<ItemInfo> accessoriesItemList = new List<ItemInfo>();

    public Dictionary<ItemType, List<ItemInfo>> itemListDic = new Dictionary<ItemType, List<ItemInfo>>();

    public void ItemDataInit()
    {
        itemListDic[ItemType.PickUp] = pickUpItemList;
        itemListDic[ItemType.Passive] = passiveItemList;
        itemListDic[ItemType.Active] = activeItemList;
        itemListDic[ItemType.Accessories] = accessoriesItemList;

    }
}
