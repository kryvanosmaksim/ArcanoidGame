using Arkanoid.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Arkanoid.Game;
using UnityEngine.Serialization;

namespace Arkanoid.Services
{
    public class LivesService : SingletonMonoBehaviour<LivesService>
    {
        [SerializeField] private GameObject[] _lifeSprites; // Assign your life sprites in the inspector
        [SerializeField] private int _maxLives = 3;
        
        private int _currentLives;
        private Ball _ball;

        protected override void Awake()
        {
            base.Awake();
            InitLives();
        }

        private void InitLives()
        {
            _currentLives = _maxLives;
            UpdateLivesDisplay();
            _ball = FindObjectOfType<Ball>();
        }

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

        private void UpdateLivesDisplay()
        {
            for (int i = 0; i < _lifeSprites.Length; i++)
            {
                if (i < _currentLives)
                {
                    _lifeSprites[i].SetActive(true);
                }
                else
                {
                    _lifeSprites[i].SetActive(false);
                }
            }
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

        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InitLives();
        }
    }
}
