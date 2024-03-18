using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameCounter : MonoBehaviour
{
    public AudioSource GameGoodSound;
    public AudioSource GameBadSound;
    public AudioSource GameStepSound;



    public RectTransform ProgressBar;
    public TextMeshProUGUI StepsText;
    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI MistakesText;

    public GameObject GameOverPanel;
    public TextMeshProUGUI GameScoreText;

    public UnityEvent<int> OnStep;


    public static GameCounter Instance
    {
        get
        {
            return GameObject.Find("GameCounter").GetComponent<GameCounter>();
        }
    }

    public GameObject Game;
    public bool countScore = true;
    public bool countMistakes = false;

    private int _steps = 0;
    public int steps
    {
        get
        {
            return _steps;
        }
        set
        {
            _steps = value;
            if (StepsText != null)
            {
                StepsText.text = _steps + "/" + maxSteps;
            }
        }

    }

    public int maxSteps = 10;

    private int _score = 0;
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            if (ScoreText != null)
            {
                ScoreText.text = "" + _score;
            }
        }
    }
    private int _mistakes = 0;
    public int mistakes
    {
        get
        {
            return _mistakes;
        }
        set
        {
            _mistakes = value;
            if (MistakesText != null)
            {
                MistakesText.text = "" + _mistakes;
            }
        }
    }
    public void Start()
    {
        if (ProgressBar != null)
        {
            ProgressBar.localScale = new Vector3(0, 1, 1);
            steps = 0;

            mistakes = 0;
            score = 0;
        }
    }
    public void Update()
    {
        ScoreText.transform.parent.gameObject.SetActive(countScore);
        MistakesText.transform.parent.gameObject.SetActive(countMistakes);

        if (ProgressBar != null)
        {
            ProgressBar.localScale = Vector3.Lerp(ProgressBar.localScale, new Vector3((float)steps / maxSteps, 1, 1), Time.deltaTime * 5);
        }
    }
    public void SwitchPanel(bool isPause)
    {
        GameOverPanel.transform.GetChild(0).GetChild(0).gameObject.SetActive(isPause);
        GameOverPanel.transform.GetChild(0).GetChild(1).gameObject.SetActive(!isPause);
    }
    public void MarkMiss()
    {
        mistakes++;
    }

    public void Answer(bool correct)
    {
        steps++;
        OnStep.Invoke(steps);
        if (correct)
        {
            if (countScore)
            {
                GameGoodSound.Play();
                score++;
            } else {
                GameStepSound.Play();
            }
        }
        else
        {
            if (countMistakes)
            {
                GameBadSound.Play();
                mistakes++;
            } else {
                Debug.Log("Step");
                GameStepSound.Play();
            }
        }
        if (steps >= maxSteps)
        {

            Game.SetActive(false);
            GameOverPanel.SetActive(true);
            SwitchPanel(false);
            string info = "";
            if (countScore)
            {
                info += "Счет: " + score + ". ";
            }
            if (countMistakes)
            {
                info += "Ошибки: " + mistakes + ".";
            }
            GameScoreText.text = info;

        }
    }
    public void Pause()
    {
       Game.SetActive(false);
        GameOverPanel.SetActive(true);
        SwitchPanel(true);
    }
    public void Resume()
    {
        Game.SetActive(true);
        GameOverPanel.SetActive(false);
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
