using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Inject]
    private GameController _gameController;


    public GameObject startPage;
    public GameObject menuPage;
    public GameObject pausePage;
    public GameObject deadPage;
    public GameObject countDownPage;
    public GameObject scoreObj;
    public GameObject PauseButton;
    public GameObject HighScore;
    public Text scoreText;
    public Text countdown;
    public Text score;


    public void Update()
    {
        if (menuPage.activeSelf)
            SetMusicOption();
    }
    public void SetMusicOption()
    {
        if (GameObject.Find("Canvas/menuPage/MusicActivate").GetComponent<Toggle>().isOn)
            PlayerPrefs.SetInt("Music", 1);
        else PlayerPrefs.SetInt("Music", 0);
    }
    public void OnPlayerDead()
    {
        _gameController.gameOver = true;
        int savedScore = PlayerPrefs.GetInt("HighScore");
        if (_gameController.score > savedScore)
        {
            PlayerPrefs.SetInt("HighScore", _gameController.score);
        }
        HighScoreChanged();
        SetPageState(GameController.PageState.GameOver);

    }
    public void OnOption()
    {
        _gameController.gameOver = true;
        SetPageState(GameController.PageState.Menu);
    }
    public void StartGame()
    {
        _gameController.gameOver = true;
        SetPageState(GameController.PageState.Start);
        HighScoreChanged();
    }
    public void OnPause()
    {
        _gameController.gameOver = true;
        SetPageState(GameController.PageState.Pause);
    }
    public void OnCountDown()
    {
        if (pausePage.activeSelf)
        _gameController.gameOver = true;
        else _gameController.gameOver = false;
        SetPageState(GameController.PageState.Countdown);
        CountdownText();
    }
    public void gameStarted()
    {
        scoreText = scoreObj.GetComponent<Text>();
        _gameController.gameOver = false;
        scoreText.text = "0";
        SetPageState(GameController.PageState.None);
    }
    public void CountdownText()
    {
        countdown = GameObject.Find("Canvas/countDownPage/CountdownText").GetComponent<Text>();
        countdown.text = "3";
        StartCoroutine("Countdown");
    }
    IEnumerator Countdown()
    {
        int count = 3;
        for (int i = 0; i < count; i++)
        {
            countdown.text = (count - i).ToString();
            yield return new WaitForSeconds(1);
        }

        _gameController.OnCountdownFinished();
    }
    public void HighScoreChanged()
    {
        score = HighScore.GetComponent<Text>();
        score.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void SetPageState(GameController.PageState state)
    {
        switch (state)
        {
            case GameController.PageState.None:
                startPage.SetActive(false);
                menuPage.SetActive(false);
                pausePage.SetActive(false);
                deadPage.SetActive(false);
                countDownPage.SetActive(false);
                scoreObj.SetActive(true);
                PauseButton.SetActive(true);
                HighScore.SetActive(false);
                break;
            case GameController.PageState.Start:
                startPage.SetActive(true);
                menuPage.SetActive(false);
                pausePage.SetActive(false);
                deadPage.SetActive(false);
                countDownPage.SetActive(false);
                scoreObj.SetActive(false);
                PauseButton.SetActive(false);
                HighScore.SetActive(true);
                break;
            case GameController.PageState.Menu:
                startPage.SetActive(false);
                menuPage.SetActive(true);
                pausePage.SetActive(false);
                deadPage.SetActive(false);
                countDownPage.SetActive(false);
                scoreObj.SetActive(false);
                PauseButton.SetActive(false);
                HighScore.SetActive(false);
                break;
            case GameController.PageState.Pause:
                startPage.SetActive(false);
                menuPage.SetActive(false);
                pausePage.SetActive(true);
                deadPage.SetActive(false);
                countDownPage.SetActive(false);
                scoreObj.SetActive(true);
                PauseButton.SetActive(false);
                HighScore.SetActive(false);
                break;
            case GameController.PageState.Countdown:
                startPage.SetActive(false);
                menuPage.SetActive(false);
                pausePage.SetActive(false);
                deadPage.SetActive(false);
                countDownPage.SetActive(true);
                scoreObj.SetActive(false);
                PauseButton.SetActive(false);
                HighScore.SetActive(false);
                break;
            case GameController.PageState.GameOver:
                startPage.SetActive(false);
                menuPage.SetActive(false);
                pausePage.SetActive(false);
                deadPage.SetActive(true);
                countDownPage.SetActive(false);
                scoreObj.SetActive(true);
                PauseButton.SetActive(false);
                HighScore.SetActive(true);
                break;
        }
    }
}
