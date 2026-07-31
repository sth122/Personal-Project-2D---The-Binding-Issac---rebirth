using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    protected bool isDDOL = true;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            if (isDDOL)
            {
                _instance = this as T;

                if (transform.parent == null)
                {
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    transform.SetParent(null);
                    DontDestroyOnLoad(gameObject);
                }
            }
            Initialize();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Initialize() { }
}