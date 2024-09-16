using Arkanoid.Game;
using Arkanoid.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid.Services
{
    public class GameService : SingletonMonoBehaviour<GameService>
    {
        #region Variables
        
        private int _score;
        private ScoreLabel _scoreText;

        #endregion

        #region Unity lifecycle

        protected override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Start()
        {
            if (LevelService.Instance != null)
            {
                LevelService.Instance.OnAllBlocksDestroyed += AllBlocksDestroyedCallback;
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            if (LevelService.Instance != null)
            {
                LevelService.Instance.OnAllBlocksDestroyed -= AllBlocksDestroyedCallback;
            }
        }

        #endregion

        #region Public methods

        public void AddScore(int points)
        {
            _score += points;
            UpdateScoreText();
        }

        #endregion

        #region Private methods

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LinkScoreText();
            ResetScore();
        }

        private void LinkScoreText()
        {
            _scoreText = FindObjectOfType<ScoreLabel>();
            if (_scoreText == null)
            {
                Debug.LogError("ScoreText not found in the scene");
            }
            else
            {
                UpdateScoreText();
            }
        }

        private void ResetScore()
        {
            _score = 0;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            _scoreText?.SetScore(_score);
        }

        private void AllBlocksDestroyedCallback()
        {
            Debug.LogError("Game Win!");
        }

        #endregion
    }
}
