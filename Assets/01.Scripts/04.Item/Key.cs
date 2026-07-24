using UnityEngine;

public class Key : Item
{
    protected PickUpType type;
    protected int count;
    private void Start()
    {
        itemName = "Key";
        count = 1;
        type = PickUpType.Key;
    }

    protected override void ItemEffect()
    {
        IsaacManager.Instance.GetPickUpItem(type, count, () => ReturnPool());
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Isaac"))
        {
            IsaacManager.Instance.GetItem(() => ItemEffect());
        }
    }
}