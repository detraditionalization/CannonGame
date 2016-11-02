using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance { get; private set; }
    public Scene CurrentLevel { get; private set; }
    public bool IsPlaying { get; set; }
    public bool GamePaused { get; set; }
    public float LevelTime { get; private set; }
    public bool SceneReloaded { get; private set; }
    public bool SceneChanged { get; private set; }

    void Awake () {
        Instance = this;
        IsPlaying = false;

        SceneManager.activeSceneChanged += SceneChange;  // subscribe to the scene change event

        CurrentLevel = SceneManager.GetActiveScene();
    }
	
	
	void Update () {


        if (!GamePaused)
        {
            //Debug
            if (Input.GetKeyDown(KeyCode.U))
            {
                CallNextLevel();
            }

            // Reset game when prompted
            if (InputHandler.ResetKey && IsPlaying && CurrentLevel.buildIndex != 0)
            {
                ReloadLevel();
            }

            // Ready screen activate
            if (InputHandler.NextLevelKey && !IsPlaying && CurrentLevel.buildIndex != 0)
            {
                IsPlaying = true;
            }

            // Go to next level when finished a level
            if (InputHandler.NextLevelKey && GameManager.Instance.IsWinner && CurrentLevel.buildIndex != 0)
            {
                CallNextLevel();
            }

            // Timer
            if (IsPlaying && !GameManager.Instance.IsWinner)
            {
                LevelTime += Time.deltaTime;
            }
            else
            {
                LevelTime = 0f;
            }
        }
       
    
	}

    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SceneChange;
        Instance = null;
    }

    void SceneChange(Scene previousScene, Scene newScene)
    {        
        LevelTime = 0f;
        GamePaused = false;
        IsPlaying = false;
        if (previousScene.buildIndex == newScene.buildIndex)
        {
            SceneReloaded = true; // indicates if we reloaded the same scene           
        }
        else
        {            
            SceneChanged = true;
        }        
    }

    public void CallNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)  // check if theres a level to load next
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // if so load that level
        }
        
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;                
    }

    public void CallLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);    
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
