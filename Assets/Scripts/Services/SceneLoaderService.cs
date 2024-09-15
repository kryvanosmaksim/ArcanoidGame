using Arkanoid.Utility;
using UnityEngine.SceneManagement;

namespace Arkanoid.Services
{
    public class SceneLoaderService : SingletonMonoBehaviour<SceneLoaderService> //system, manager
    {
        #region Public methods

        public static void LoadNextLevelTest()
        {
            //todo: Remove it!
            SceneManager.LoadScene("Level002");
        }

        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        #endregion
    }
}