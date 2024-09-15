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

        private int _maxLives;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            OnCreated?.Invoke(this);
            _maxLives = _lives;
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
            _lives--;
            UpdateDamageState();
            if (_lives <= 0)
            {
                GameService.Instance.AddScore(_points); // Updated reference
                Destroy(gameObject);
            }
        }

        private void UpdateDamageState()
        {
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

        #endregion
    }
}