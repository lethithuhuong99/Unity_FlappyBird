using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePLayController : MonoBehaviour
{
    public static GamePLayController instance;

    [SerializeField]
    private Button instructionButton;

    [SerializeField]
    private Text

            scoreText,
            endScoreText,
            bestScoreText;

    [SerializeField]
    private GameObject

            gameOverPanel,
            pausePanel,
            pauseButton;

    void Awake()
    {
        Time.timeScale = 0;
        _MakeInstance();
    }

    void _MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void _InstructionButton()
    {
        Time.timeScale = 1;
        instructionButton.gameObject.SetActive(false);
    }

    public void _SetScore(int score)
    {
        scoreText.text = "" + score;
    }

    public void _BirdDiedShowPanel(int score)
    {
        endScoreText.text = "" + score;
        if (score > GameManager.instance.GetHighScore())
        {
            GameManager.instance.SetHighScore (score);
        }
        bestScoreText.text = "" + GameManager.instance.GetHighScore();
        gameOverPanel.SetActive(true);
    }

    public void _MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void _RestartGame()
    {
        // get the current scene name
        string sceneName = SceneManager.GetActiveScene().name;

        // load the same scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void _PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void _ResumeButton()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }
}
