using System;
using UnityEngine;

namespace Arkanoid.Game
{
    public class Ball : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Vector2 _startDirection;

        private void Start()
        {
            _rb.velocity = _startDirection;
        }

        #endregion
    }
}