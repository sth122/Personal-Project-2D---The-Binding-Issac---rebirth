using UnityEngine;

public interface IChangeStat
{
    public void ChangeStat();
}

public interface IDropable
{
    public void OnDrop();
}

public interface IPickUpAnimable
{
    public void PickUpAnima();
}

abstract public class Item : MonoBehaviour
{

    protected string itemName;
    // 각각의 아이템 효과 추상 메서드
    protected abstract void ItemEffect();
}
