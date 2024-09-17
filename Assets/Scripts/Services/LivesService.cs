using System.Collections;
using Arkanoid.Game;
using Arkanoid.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid.Services
{
    public class LivesService : SingletonMonoBehaviour<LivesService>
    {
        #region Variables

        [SerializeField] private GameObject[] _lifeSprites;
        [SerializeField] private int _maxLives = 3;
        private Ball _ball;

        private int _currentLives;

        #endregion

        #region Unity lifecycle

        protected override void Awake()
        {
            base.Awake();
            InitLives();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        #endregion

        #region Public methods

        public void LoseLife()
        {
            if (_currentLives > 0)
            {
                _currentLives--;
                UpdateLivesDisplay();

                if (_currentLives > 0)
                {
                    ResetBall();
                }
                else
                {
                    ReloadLevel();
                }
            }
        }

        #endregion

        #region Private methods

        private void InitLives()
        {
            _currentLives = _maxLives;
            UpdateLivesDisplay();
            _ball = FindObjectOfType<Ball>();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InitLives();
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void ResetBall()
        {
            StartCoroutine(ResetBallCoroutine());
        }

        private IEnumerator ResetBallCoroutine()
        {
            _ball.ResetBall();
            yield return new WaitForSeconds(0.5f);
        }

        private void UpdateLivesDisplay()
        {
            for (int i = 0; i < _lifeSprites.Length; i++)
            {
                _lifeSprites[i].SetActive(i < _currentLives);
            }
        }

        #endregion
    }
}