using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{
    public GameObject AboutPanel;

    private void Awake()
    {
    }

    private void Update()
    {
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CallLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void ShowAbout()
    {
        AboutPanel.SetActive(true);
    }
}