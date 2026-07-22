using UnityEngine;

public class Key : MonoBehaviour, IReturnPool
{
    public void ReturnPool()
    {
        ObjectPoolManager.Instance.ReturnObject("Key", this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Isaac"))
        {
            IsaacManager.Instance.GetItem();
            ReturnPool();
        }
    }
}