using UnityEngine;

public class TreasureDoor : Door
{
    protected override void Awake()
    {
        doorType = DoorType.TreasureDoor;
        base.Awake();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
