using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
{
    static T Instance;
    public static T I
    {
        get
        {
            if (Instance != null) return Instance;
            
            Instance = (T)FindAnyObjectByType(typeof(T));
            if (Instance == null) SetupInstance();
            
            Instance.name = $"[Captured] {Instance.name}";
            return Instance;
        }
    }
    
    static void SetupInstance()
    {
        var gameObj = new GameObject
        {
            name = typeof(T).Name
        };

        Instance = gameObj.AddComponent<T>();
        DontDestroyOnLoad(gameObj);
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"Instance Duplicated : {this.name}");
            Destroy(gameObject);
        }
    }
}
