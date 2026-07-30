using UnityEngine;

public class NormalDoor : Door
{
    protected override void Awake()
    {
        doorType = DoorType.NormalDoor;
        base.Awake();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
