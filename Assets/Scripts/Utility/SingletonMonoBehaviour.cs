using UnityEngine;

namespace Arkanoid.Utility
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        private static object _lock = new object();

        protected virtual void Awake()
        {
            lock (_lock)
            {
                if (Instance != null && Instance != this as T)
                {
                    Destroy(gameObject);
                    return;
                }

                Instance = this as T;
                DontDestroyOnLoad(gameObject);
                transform.SetParent(null);
            }
        }
    }
}