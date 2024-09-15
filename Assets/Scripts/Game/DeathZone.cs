using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid.Game
{
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private bool _isActive = true;
        
        #region Unity lifecycle

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_isActive)
            {
                return;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        #endregion
    }
}