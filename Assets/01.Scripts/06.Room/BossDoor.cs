using UnityEngine;

public class BossDoor : Door
{
    protected override void Awake()
    {
        doorType = DoorType.BossDoor;
        base.Awake();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
