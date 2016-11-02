using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public bool GameIsPaused { get; private set; }
    public bool SceneChanged { get; private set; }
    public bool SceneReloaded { get; private set; }
    public float WinTime { get; private set; }
    private Text _comboText;
    private Text _greensText;

    private Text _levelText;
    private GameObject _pausePanel;
    private Text _readyText;
    private Text _timeText;
    private Text _winText;

    private void Awake()
    {
        Instance = this;
        SceneManager.activeSceneChanged += SceneChange;  // subscribe to the scene change event

        _timeText = GameObject.Find("TimeText").GetComponent<Text>();
        _winText = GameObject.Find("WinText").GetComponent<Text>();
        _greensText = GameObject.Find("GreensText").GetComponent<Text>();

        _levelText = GameObject.Find("LevelText").GetComponent<Text>();
        _readyText = GameObject.Find("ReadyText").GetComponent<Text>();
        _comboText = GameObject.Find("ComboText").GetComponent<Text>();
        _pausePanel = GameObject.Find("PausePanel");

        _comboText.text = "";
        _timeText.text = "";
        _winText.text = "";
        _greensText.text = "";

        _levelText.text = "";
        _readyText.text = "PRESS 'E' WHEN READY";

        _readyText.enabled = false;
        _pausePanel.SetActive(false);   // pause menu is disabled at first
    }

    private void Update()
    {
        // by default nothing is shown
        _winText.enabled = false;
        _timeText.enabled = false;
        _greensText.enabled = false;
        _levelText.enabled = false;
        _readyText.enabled = false;
        _comboText.enabled = false;

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            // Pause functionality: Pause menu doesn't work in main menu
            if (InputHandler.PauseKey)
            {
                if (!_pausePanel.activeInHierarchy)
                {
                    _pausePanel.SetActive(true);
                    LevelManager.Instance.GamePaused = true;
                    Time.timeScale = 0f;
                }
                else
                {
                    _pausePanel.SetActive(false);
                    LevelManager.Instance.GamePaused = false;
                    Time.timeScale = 1f;
                }
            }

            if (!LevelManager.Instance.IsPlaying)
            {
                ShowReadyScreen(); // before the game starts shows the ready screen

                _winText.enabled = false;
                _timeText.enabled = false;
                _greensText.enabled = false;
                _levelText.enabled = false;
                _comboText.enabled = false;
            }
            else
            {
                ShowComboText();
                ShowWinText();
                ShowTimeText();
                ShowGreensText();
                ShowLevelText();
                _readyText.enabled = false;
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SceneChange;
        Instance = null;
    }

    private void SceneChange(Scene previousScene, Scene newScene)
    {
        LevelManager.Instance.IsPlaying = false;
        _pausePanel.SetActive(false); // on scene change pause menu always diassapears
        Ball.Combo = 0;   // reset the combo
        if (previousScene.buildIndex == newScene.buildIndex)
        {
            SceneReloaded = true; // indicates if we reloaded the same scene
            _winText.enabled = false;
        }
        else
        {
            SceneChanged = true;
            _readyText.enabled = true;
        }
    }

    private void ShowComboText()
    {
        if (Ball.Combo >= 2)
        {
            _comboText.enabled = true;
            _comboText.text = Ball.Combo.ToString() + " X";
            if (Ball.Combo >= 5)
            {
                _comboText.text = Ball.Combo.ToString() + " X " + "\r\n" + "Great!";
            }
            if (Ball.Combo >= 10)
            {
                _comboText.text = Ball.Combo.ToString() + " X " + "\r\n" + "Groovy!";
            }
            if (Ball.Combo >= 20)
            {
                _comboText.text = Ball.Combo.ToString() + " X " + "\r\n" + "Shagalicious!";
            }
            if (Ball.Combo >= 30)
            {
                _comboText.text = Ball.Combo.ToString() + " X " + "\r\n" + "OH BEHAVE ;)";
            }

            _comboText.color = Color.HSVToRGB((Time.unscaledTime * 0.4f) % 1f, 1f, 1f);
            _comboText.fontSize = Mathf.Min(Ball.Combo * 5 + 30, 90);           // clamp the value between min and max value of the text
            _comboText.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Ball.Combo) * 20f);  // it has a random rotation between -20 & 20 degrees
            TextWobble(_comboText, Mathf.Min(15, Ball.Combo));
        }
    }

    private void ShowGreensText()
    {
        _greensText.enabled = true;
        _greensText.text = "Greens : " + GameManager.Instance.ActiveGreens.ToString() + " / " + GameManager.Instance.ObstacleNumber.ToString();
    }

    private void ShowLevelText()
    {
        _levelText.enabled = true;
        _levelText.text = "Level : " + SceneManager.GetActiveScene().name;
    }

    private void ShowReadyScreen()
    {
        _readyText.enabled = true;
        TextWobble(_readyText);
        if (InputHandler.NextLevelKey && !LevelManager.Instance.GamePaused)
        {
            LevelManager.Instance.IsPlaying = true;
        }
    }

    private void ShowTimeText()
    {
        if (!GameManager.Instance.IsWinner)
        {
            _timeText.enabled = true;
            _timeText.text = "Time : " + LevelManager.Instance.LevelTime.ToString("n2") + " secs";
            WinTime = LevelManager.Instance.LevelTime;
            TextWobble(_timeText);
        }
        else
        {
            _timeText.text = "Time : " + WinTime.ToString("n2") + " secs";
        }
    }

    private void ShowWinText()
    {
        if (GameManager.Instance.IsWinner)
        {
            _winText.GetComponent<Text>().text = "You won in " + WinTime.ToString("n2") + " seconds !" + "\r\n" + "'E' : Next Level" + "\r\n" + "'R' : Replay";
            _winText.enabled = true;
            _timeText.enabled = false;

            //_winText.transform.DOScale(new Vector3(1f, 1f, 0f), 1f).SetAutoKill(true).OnComplete(() => TextWobble(_winText));
            TextWobble(_winText);
        }
    }

    private void TextWobble(Text textComponent, float speed = 4)
    {
        textComponent.transform.localScale = new Vector3(1f + Mathf.Abs(Mathf.Sin(Time.unscaledTime * speed)) * 0.2f, 1f + Mathf.Abs(Mathf.Sin(Time.unscaledTime * speed)) * 0.1f, 0f);
    }
}