using System;
using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game
{
    public class Block : MonoBehaviour
    {
        public static event Action<Block> OnCreated;
        public static event Action<Block> OnDestroyed;
        
        #region Variables
        
        [SerializeField] private GameObject[] _damageStates;
        [SerializeField] private int _lives = 1;
        [SerializeField] private int _points = 100;
        [SerializeField] private bool _isIndestructible = false; // Indestructible flag
        [SerializeField] private bool _isInitiallyInvisible = false; // Initially invisible flag

        private int _maxLives;
        private bool _hasBeenHit = false;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            OnCreated?.Invoke(this);
            _maxLives = _lives;

            // Set all damage states to inactive if the block is initially invisible
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
                // Reveal the block (set the first damage state active)
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
                    GameService.Instance.AddScore(_points); // Re-add the score increment call
                    Destroy(gameObject);
                }
            }
        }

        private void UpdateDamageState()
        {
            if (_isInitiallyInvisible && !_hasBeenHit) return;

            int stateIndex = Mathf.Clamp(_maxLives - _lives, 0, _damageStates.Length - 1);

            foreach (GameObject state in _damageStates)
            {
                state.SetActive(false);
            }

            if (_damageStates.Length > stateIndex)
            {
                _damageStates[stateIndex].SetActive(true);
            }
            else
            {
                Debug.LogError("Damage state index out of range.");
            }
        }

        private void SetAllDamageStatesActive(bool isActive)
        {
            foreach (GameObject state in _damageStates)
            {
                state.SetActive(isActive);
            }
        }

        #endregion
    }
}
