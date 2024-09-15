using System;
using Arkanoid.Utility;
using UnityEngine;

namespace Arkanoid.Services
{
    public class PauseService : SingletonMonoBehaviour<PauseService>
    {
        private bool _isPaused;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        private void TogglePause()
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0 : 1; // тернарная операция
        }
    }
}