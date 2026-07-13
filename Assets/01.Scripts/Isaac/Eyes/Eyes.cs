using UnityEngine;

public class Eyes : IsaacWeapon
{
    [SerializeField] Transform[] firePosition;
    [SerializeField] GameObject tearBullet;

    protected override void Start()
    {
        base.Start();
    }

    protected void Update()
    {
        LookHead();
        Attack();
    }

    protected override void Attack()
    {

    }
}
