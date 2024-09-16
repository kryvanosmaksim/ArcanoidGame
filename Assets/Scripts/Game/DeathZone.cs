using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game
{
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private bool _isActive = true;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_isActive) return;
            SceneLoaderService.Instance.ReloadCurrentScene();
        }
    }
}