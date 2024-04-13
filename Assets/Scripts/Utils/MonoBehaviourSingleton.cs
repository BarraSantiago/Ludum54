using UnityEngine;

namespace Utils
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
    {
        private static T Instance;
        public bool dontDestroyOnLoad = false;
        public static T Get()
        {
            return Instance;
        }

        public virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                if (dontDestroyOnLoad) DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}