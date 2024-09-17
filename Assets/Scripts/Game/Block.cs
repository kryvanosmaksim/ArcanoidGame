using System;
using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game
{
    public class Block : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject[] _damageStates;
        [SerializeField] private int _lives = 1;
        [SerializeField] private int _points = 100;
        [SerializeField] private bool _isIndestructible;
        [SerializeField] private bool _isInitiallyInvisible;
        private bool _hasBeenHit;

        private int _maxLives;

        #endregion

        #region Events

        public static event Action<Block> OnCreated;
        public static event Action<Block> OnDestroyed;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            OnCreated?.Invoke(this);
            _maxLives = _lives;

            if (_isInitiallyInvisible)
            {
                SetAllDamageStatesActive(false);
            }

            UpdateDamageState();
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HandleHit();
        }

        #endregion

        #region Private methods

        private void HandleHit()
        {
            if (_isInitiallyInvisible && !_hasBeenHit)
            {
                _hasBeenHit = true;
                SetAllDamageStatesActive(false);
                _damageStates[0].SetActive(true);
                return;
            }

            if (!_isIndestructible)
            {
                _lives--;
                UpdateDamageState();
                if (_lives <= 0)
                {
                    GameService.Instance.AddScore(_points);
                    Destroy(gameObject);
                }
            }
        }

        private void SetAllDamageStatesActive(bool isActive)
        {
            foreach (GameObject state in _damageStates)
            {
                state.SetActive(isActive);
            }
        }

        private void UpdateDamageState()
        {
            if (_isInitiallyInvisible && !_hasBeenHit)
            {
                return;
            }

            int stateIndex = Mathf.Clamp(_maxLives - _lives, 0, _damageStates.Length - 1);

            foreach (GameObject state in _damageStates)
            {
                state.SetActive(false);
            }

            if (_damageStates.Length > stateIndex)
            {
                _damageStates[stateIndex].SetActive(true);
            }
        }

        #endregion
    }
}