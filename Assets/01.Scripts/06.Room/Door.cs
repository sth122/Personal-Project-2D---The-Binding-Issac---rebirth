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

    private void Awake()
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

    public void SetRotation()
    {
        switch(this.dir)
        {
            case DirectionsEnum.Up:
                
                break;
            case DirectionsEnum.Down:
                break;
            case DirectionsEnum.Left:
                break;
            case DirectionsEnum.Right:
                break;
        }
    }
}
