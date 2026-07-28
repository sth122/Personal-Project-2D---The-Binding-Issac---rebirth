using UnityEngine;

public enum DoorType
{
    NormalDoor, BossDoor, TreasureDoor, ShopDoor, SecretDoor
}

abstract public class Door : MonoBehaviour
{
    protected string d_Name;
    protected Animator animator;
    protected DirectionsEnum dir;
    protected RoomType roomType;
    protected DoorType doorType;
    protected Room currentRoom;
    protected Room nextRoom;

    protected virtual void Awake()
    {
        d_Name = doorType.ToString();
        animator = GetComponent<Animator>();
    }

    public void OnCloseDoor()
    {
        animator.SetBool("isEnter", true);
    }
    public void OnOpneDoor()
    {
        animator.SetBool("isClear", true);
    }

    public void SetTransform(DoorPos pos)
    {
        dir = pos.direction;
        transform.position = currentRoom.transform.position + pos.postion;
        transform.rotation = pos.rotation;
    }
    public void SetRoom(Room currentRoom, Room nextRoom)
    {
        this.currentRoom = currentRoom;
        this.nextRoom  = nextRoom;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Isaac"))
        {
            RoomManager.Instance.ChangeRoom(nextRoom, collision.gameObject, dir);
        }
    }
}
