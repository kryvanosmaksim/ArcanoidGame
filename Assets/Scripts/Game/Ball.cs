using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid.Game
{
    public class Ball : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private Collider2D _bottomWallCollider;

        private bool _isStarted;
        private Platform _platform;
        private Vector2 _startDirection;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _platform = FindObjectOfType<Platform>();
            _startDirection = GetRandomStartDirection();
        }

        private void Update()
        {
            if (_isStarted)
            {
                return;
            }

            MoveWithPlatform();

            if (Input.GetMouseButtonDown(0))
            {
                StartFlying();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider == _bottomWallCollider)
            {
                ReloadLevel();
            }
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying && !_isStarted)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)_startDirection);
            }
            else if (Application.isPlaying)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)_rb.velocity);
            }
        }

        #endregion

        #region Private methods

        private Vector2 GetRandomStartDirection()
        {
            Vector2 randomDirection = new(Random.Range(-1f, 1f), Random.Range(0.5f, 1f));
            return randomDirection.normalized;
        }

        private void MoveWithPlatform()
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x = _platform.transform.position.x;
            transform.position = currentPosition;
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void StartFlying()
        {
            _isStarted = true;
            _rb.velocity = _startDirection * _speed;
        }

        #endregion
    }
}