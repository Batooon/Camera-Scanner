using UnityEngine.SceneManagement;

namespace Code
{
    public static class SceneLoader
    {
        public static void LoadScene(string nextScene)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}