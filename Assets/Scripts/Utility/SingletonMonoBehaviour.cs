using UnityEngine;

namespace Arkanoid.Utility
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Variables

        private static readonly object _lock = new();

        #endregion

        #region Properties

        public static T Instance { get; private set; }

        #endregion

        #region Unity lifecycle

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

        #endregion
    }
}