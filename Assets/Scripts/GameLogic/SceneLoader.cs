using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 0;
    }
}
