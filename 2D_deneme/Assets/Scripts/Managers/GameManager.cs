using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsWinner { get; private set; }

    public int ObstacleNumber;
    public int ActiveGreens;

    private Array _obstacleList;

    private void Awake()
    {
        Instance = this;
        IsWinner = false;

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }

    private void OnDestroy()
    {
        Instance = null;
        SceneManager.activeSceneChanged -= SceneChange;
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneChange;
        ObstacleNumber = FindObjectsOfType<Obstacle>().Length;
        _obstacleList = FindObjectsOfType<Obstacle>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            CheckWin();
        }
    }

    private void CheckWin()
    {
        ActiveGreens = 0;
        foreach (Obstacle item in _obstacleList)
        {
            if (item.IsGreen)
            {
                ActiveGreens++;
            }
        }
        if (ActiveGreens == ObstacleNumber)
        {
            IsWinner = true;
        }
    }

    private void SceneChange(Scene PreviousScene, Scene NewScene)
    {
        IsWinner = false;
        ActiveGreens = 0;
        ObstacleNumber = FindObjectsOfType<Obstacle>().Length;
        _obstacleList = FindObjectsOfType<Obstacle>();
    }
}