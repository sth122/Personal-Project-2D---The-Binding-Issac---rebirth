using UnityEngine;
using System.Collections;

public class IsaacBullet : MonoBehaviour, IReturnPoolable
{
    #region variable
    protected Rigidbody2D rb;
    protected WaitForSeconds wait;
    protected Vector2 dir;
    protected float lifeTime;
    protected float speed;
    protected float gravityDelay;
    [SerializeField] protected float timer;
    protected float damage;

    #endregion

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lifeTime = 2f;
        speed = 5f;
    }

    /// <summary>
    /// 캐릭터 스탯에 있는 데이터를 받아와 연사 속도 / 딜레이 / 중력 딜레이 계산 필요
    /// 나중에 SetLifeTime / SetShootSpeed / SetGravityDelay 등등 만들어야 함
    /// </summary>

    protected virtual void OnEnable()
    {
        gravityDelay = lifeTime - 0.5f;
        wait = new WaitForSeconds(gravityDelay);
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        timer = 0f;
        StartCoroutine(GravityDelay());
    }


    protected virtual void Start()
    {

    }

    protected virtual void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        rb.linearVelocity = dir * speed;
    }

    protected virtual void Update()
    {
        if (timer > lifeTime)
        {
            ReturnPool();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            //collision.gameObject.GetCompnent<MonsterController>().TakeDamage(damage);
            ReturnPool();
        }
    }

    /// <summary>
    /// 캐릭터 눈물 공식 계산 후 적용
    /// </summary>
    /// <param name="damage"></param>
    public void SetDamage(float damage) { this.damage = damage; }

    /// <summary>
    /// 캐릭터 공격 방향 벡터 설정
    /// </summary>
    /// <param name="dir"></param>
    public void SetDirection(Vector2 dir)
    {
        this.dir = dir;
    }
    public virtual void ReturnPool()
    {
        // 각 눈물마다
        // 눈물 사운드 이팩트 추가
        // 눈물 충돌 이펙트(anim) 추가
        ObjectPoolManager.Instance.ReturnObject("IsaacBullet", this.gameObject);
    }

    IEnumerator GravityDelay()
    {
        yield return wait;
        rb.gravityScale = 10f;
    }
}
