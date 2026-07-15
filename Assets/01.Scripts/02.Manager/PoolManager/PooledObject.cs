using UnityEngine;

public class PooledObject<T> : MonoBehaviour where T : Component
{
    public T Component {  get; private set; }

    private ObjectPoolManager<PooledObject<T>> poolManager;
    private string poolName;

    public void Initialize(ObjectPoolManager<PooledObject<T>> manager, string name)
    {
        Component = GetComponent<T>();

        if(Component == null)
        {
            Debug.LogError($"{gameObject.name}에 {typeof(T).Name} 없음");
        }

        Debug.Log($"{gameObject.name}에 {typeof(T).Name} 가져옴");

        poolManager = manager;
        poolName = name;
    }

    protected void ReturnToPool()
    {
        poolManager.Return(poolName, this);
    }
}
