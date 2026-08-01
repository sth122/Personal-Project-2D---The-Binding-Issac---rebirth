using UnityEngine;

public class EscapeDoor : Door
{
    protected override void Awake()
    {
        doorType = DoorType.EscapeDoor;
        base.Awake();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Isaac"))
        {
            collision.gameObject.transform.position = transform.position;
            IsaacManager.Instance.GoToNextStage();
        }
    }

    public override void SetRoom(Room currentRoom, Room nextRoom)
    {
        this.currentRoom = currentRoom;
        this.nextRoom = null;
    }

    public override void SetTransform(DoorPos pos)
    {
        base.SetTransform(pos);
        transform.rotation = Quaternion.identity;
    }
}
