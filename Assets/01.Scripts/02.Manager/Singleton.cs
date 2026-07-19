using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private bool dontDestroy = true;
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = InitManager<T>();
            }
            return instance;
        }
    }

    protected static U InitManager<U>() where U : MonoBehaviour
    {
        GameObject go = null;
        U obj = FindAnyObjectByType<U>();
        if(obj == null)
        {
            go = new GameObject(typeof(U).Name);
            go.AddComponent<U>();
        }
        else
        {
            go = obj.gameObject;
        }

        return obj.GetComponent<U>();
    }

    private void Awake()
    {
        if(instance == null)
        {
            if(dontDestroy)
            {
                Instance.Initialize();
            }
            else
            {
                instance = GetComponent<T>();
                Initialize();
            }
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void Initialize() { }
}