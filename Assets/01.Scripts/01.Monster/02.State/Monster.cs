using System;
using System.Collections;
using UnityEngine;

abstract public class Monster : MonoBehaviour, IReturnPool
{
    #region variable
    protected EntityType type = EntityType.Monster;

    [SerializeField] protected Transform target;

    protected Rigidbody2D rb;

    [SerializeField] protected MonsterInfo mData;

    protected MonsterAnimController animController;
    public MonsterAnimController AnimController { get { return animController; } }

    protected SpriteRenderer sr;
    private WaitForSeconds wait;
    #endregion

    protected virtual void Awake()
    {
        animController = GetComponent<MonsterAnimController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

    }

    protected virtual void OnEnable()
    {
        if (mData != null)
        {
            Appear();
        }
    }

    protected virtual void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void InitData(MonsterInfo data, Transform target)
    {
        mData = data.Clone();
        mData.SetTotalHp();
        OnDataLodead();
        this.target = target;
        Appear();
    }

    protected virtual void OnDataLodead() { }
    protected abstract void Appear();

    public abstract void Dead();

    public void ReturnPool()
    {
        RoomManager.Instance.currentRoom.OnObjectReturn(this.gameObject);
        ObjectPoolManager.Instance.ReturnObject(mData.name, this.gameObject);
    }
    public void StartAnimTime(float time, Action OnComplete)
    {
        Debug.Log("StartAnimTime에 진입");
        StartCoroutine(AnimTime(time, OnComplete));
    }

    IEnumerator AnimTime(float time, Action OnComplete)
    {
        wait = new WaitForSeconds(time);
        yield return wait;
        OnComplete?.Invoke();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Isaac") &&
            collision.gameObject.TryGetComponent<ITakeDamageable>(out ITakeDamageable isaac))
        {
            isaac.TakeDamage(mData.contactDamage, rb.linearVelocity);
        }
    }


}
