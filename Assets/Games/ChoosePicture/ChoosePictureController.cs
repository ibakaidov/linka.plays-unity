using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePictureController : ChooseController<WordSObject>
{
    
    private bool ChoosePictures = true;
    public AudioSource WhereIsAudio;
    public AudioSource IncorrectAudio;
    public AudioSource CorrectAudio;
    public TextMeshProUGUI QuestionText;
    public Button[] buttons;

    private List<int> QuestionSkips = new List<int>();

    private AudioSource whereIsChildAudio
    {
        get
        {
            return WhereIsAudio.transform.GetChild(0).GetComponent<AudioSource>();
        }
    }
    private AudioSource incorrectChildAudio
    {
        get
        {
            return IncorrectAudio.transform.GetChild(0).GetComponent<AudioSource>();
        }
    }
    void Start()
    {
        FillUI();
    }

     public void FillUI()
    {
        var level = FillLevel();
        QuestionText.text = GetQuestionText(level.question);
        buttons[0].transform.parent.gameObject.SetActive(false);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = GetOption(level.options[i]);
            var index = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() =>
            {
                if (index == level.correctIndex)
                {
                    buttons[0].transform.parent.gameObject.SetActive(false);
                    GameCounter.Instance.Answer(true);
                    CorrectAudio.Play();
                }
                else
                {
                    buttons[0].transform.parent.gameObject.SetActive(false);
                    incorrectChildAudio.clip = level.options[index].clip;
                    GameCounter.Instance.MarkMiss();
                    IncorrectAudio.Play();
                }
            });
        }
        WhereIsAudio.Play();
        whereIsChildAudio.clip = level.question.clip;
    }

    public override Level FillLevel()
    {
        var question = GetRandomWord(QuestionSkips.ToArray());
        var options = new WordSObject[buttons.Length];
        options[0] = question;
        QuestionSkips.Add(System.Array.IndexOf(Items, question));

        int[] skips = new int[buttons.Length + 1];
        skips[0] = System.Array.IndexOf(Items, question);
        for (int i = 1; i < options.Length; i++)
        {
            options[i] = GetRandomWord(skips);
            skips[i] = System.Array.IndexOf(Items, options[i]);

        }
        var correctIndex = Random.Range(0, options.Length);
        var temp = options[correctIndex];
        options[correctIndex] = options[0];
        options[0] = temp;

        return new Level(question, options, correctIndex);
    }

    public override string GetOption(WordSObject word)
    {
        if (ChoosePictures)
        {
            return word.emoji;
        }
        else
        {
            return word.word;
        }
    }

    public override string GetQuestionText(WordSObject word)
    {
        if (ChoosePictures)
        {
            return word.word;
        }
        else
        {
            return word.emoji;
        }
    }

    protected override WordSObject[] LoadItems()
    {
        return WordBank.GetAllWords();
    }
    
    public void SetChoosePictures(bool value)
    {
        ChoosePictures = value;
        FillUI();
    }
}
