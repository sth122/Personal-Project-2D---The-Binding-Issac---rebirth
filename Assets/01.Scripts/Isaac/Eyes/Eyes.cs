using UnityEngine;

public class Eyes : IsaacWeapon
{
    [SerializeField] Transform[] firePosition;
    [SerializeField] GameObject tearBullet;

    protected void Start()
    {
        base.Start();
    }

    protected void Update()
    {
        Attack();
    }

    protected override void Attack()
    {

    }
}
