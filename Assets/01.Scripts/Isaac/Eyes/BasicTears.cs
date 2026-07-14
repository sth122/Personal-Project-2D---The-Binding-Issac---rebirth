using UnityEngine;

public class BasicTears : IsaacBullet
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public override void ReturnPool()
    {

        // 눈물 사운드 이팩트 추가
        // 눈물 충돌 이펙트(anim) 추가
        ObjectPoolManager.Instance.ReturnObject("BasicTears", this.gameObject);
    }
}
