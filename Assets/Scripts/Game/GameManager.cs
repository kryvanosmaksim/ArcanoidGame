using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Variables

        public static GameManager Instance; //разберем на лекции
        private int _score;

        private ScoreLabel _scoreText;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
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

        private void LinkScoreText()
        {
            _scoreText = FindObjectOfType<ScoreLabel>();
            if (_scoreText == null)
            {
                Debug.LogError(
                    "ScoreText not found in the scene");
            }

            UpdateScoreText();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LinkScoreText();
            ResetScore();
        }

        private void ResetScore()
        {
            _score = 0;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            if (_scoreText != null)
            {
                _scoreText.SetScore(_score);
            }
        }

        #endregion
    }
}