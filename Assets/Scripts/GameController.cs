using UnityEngine;
using Zenject;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [Inject]
    private GameConfig _config;
    [Inject]
    private PlayerController _playerController;
    [Inject]
    private UIController _uiController;
    [Inject]
    private Parallaxer _parallaxer;


    public enum PageState
    {
        None,
        Start,
        Menu,
        Pause,
        Countdown,
        GameOver
    }

    public int score;
    public bool gameOver = true;
    private bool temp;
    private void Start()
    {
        ReturnButtonOnClick();
     }
    private void Update()
    {   if (gameOver == true) return;
        else if (gameOver == false && _uiController.countDownPage.activeSelf)
            _playerController.OnGameOverConfirmed();
        else _playerController.BirbMove();
        Debug.Log(PlayerPrefs.GetInt("Music"));
    }
    public void ExitButtonOnClick()
    {
        Application.Quit();
    }
    public void OnPlayerScored()
    {
        score++;
        _uiController.scoreText.text = score.ToString();
    }

    public void PlayButtonOnCLick()
    {
        _uiController.OnCountDown();
        _playerController.OnGameOverConfirmed();
        temp = true;
    }
    public void OptionButtonOnClick()
    {
        _uiController.OnOption();
    }
    public void PauseButtonOnClick()
    {
        _uiController.OnPause();
        _playerController.OnGameOverConfirmed();
    }
    public void ResumeButtonOnClick()
    {
        _uiController.OnCountDown();
    }
    public void ReturnButtonOnClick()
    { 
        score = 0;
        _uiController.StartGame();
        _parallaxer.PoolObjDispose();
        _playerController.GetBirbState();
        _playerController.OnGameStarted();
        _playerController.OnGameOverConfirmed();
    }
    public void OnCountdownFinished()
    { 
        score = 0;
        _uiController.gameStarted();
        if (temp)
        {
           _playerController.Playable();
            temp = false;
        }
        else
        {
            _playerController.AfterPause();
        }
    }
}
