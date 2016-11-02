using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtonManager : MonoBehaviour
{
    private void Awake()
    {
    }

    private void Update()
    {
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        LevelManager.Instance.GamePaused = false;
    }

    public void CallLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        Time.timeScale = 1f;
        LevelManager.Instance.GamePaused = false;
    }
}