using TMPro;
using UnityEngine;

namespace Arkanoid.Game
{
    public class ScoreLabel : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TMP_Text _label;

        #endregion

        #region Public methods

        public void SetScore(int score)
        {
            _label.text = $"Score: {score}";
        }

        #endregion
    }
}