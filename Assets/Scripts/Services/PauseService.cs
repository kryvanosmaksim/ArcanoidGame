using Arkanoid.Utility;
using UnityEngine;

namespace Arkanoid.Services
{
    public class PauseService : SingletonMonoBehaviour<PauseService>
    {
        public bool IsPaused { get; private set; }
        [SerializeField] private GameObject _pausePanel;

        protected override void Awake()
        {
            base.Awake();
            if (_pausePanel != null)
            {
                _pausePanel.SetActive(false); 
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        private void TogglePause()
        {
            IsPaused = !IsPaused;
            Time.timeScale = IsPaused ? 0 : 1;

            if (_pausePanel)
            {
                _pausePanel.SetActive(IsPaused);
            }
        }
    }
}