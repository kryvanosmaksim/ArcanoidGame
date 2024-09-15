using UnityEngine;

namespace Arkanoid.Utility
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            Instance = gameObject.GetComponent<T>();
        }
    }
}