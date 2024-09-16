using UnityEngine;

namespace Arkanoid.Game
{
    public class Ball : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed = 10f;

        private bool _isStarted;
        private Platform _platform;
        private Vector2 _startDirection;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _platform = FindObjectOfType<Platform>();
            _startDirection = GetRandomStartDirection();
            ResetBall();
        }

        private void Update()
        {
            if (_isStarted) return;

            MoveWithPlatform();

            if (Input.GetMouseButtonDown(0))
            {
                StartFlying();
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

        public void ResetBall()
        {
            _isStarted = false;
            _rb.velocity = Vector2.zero;
            transform.position = _platform.transform.position + new Vector3(0, 1f, 0); // Adjust as necessary
            _startDirection = GetRandomStartDirection();
        }

        private void MoveWithPlatform()
        {
            Vector3 currentPosition = transform.position;
            currentPosition.x = _platform.transform.position.x;
            transform.position = currentPosition;
        }

        private void StartFlying()
        {
            _isStarted = true;
            _rb.velocity = _startDirection * _speed;
        }

        private Vector2 GetRandomStartDirection()
        {
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f));
            return randomDirection.normalized;
        }

        #endregion
    }
}
