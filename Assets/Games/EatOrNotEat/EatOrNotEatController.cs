using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EatOrNotEatController : ChooseController<WordSObject>
{
    public TextMeshProUGUI itemText;
    public GameObject AnswersGroup;
    public AudioSource audioSource;
    WordSObject[] eatWords;
    WordSObject[] notEatWords;
    List<int> usedEatWords = new List<int>();
    List<int> usedNotEatWords = new List<int>();
    private Level level;

    void Start()
    {
        LoadItems();
        FillUI();

    }
    void FillUI()
    {
        level = FillLevel();
        audioSource.clip = level.question.clip;
        AnswersGroup.SetActive(false);
        audioSource.Play();
        itemText.text = GetQuestionText(level.question);

    }
    public void Answer(int index)
    {
        if (level.correctIndex == index)
        {
            GameCounter.Instance.Answer(true);
        }
        else
        {
            GameCounter.Instance.Answer(false);
        }
        FillUI();
    }
    public override Level FillLevel()
    {
        bool isEat = Random.Range(0, 2) == 0;
        WordSObject word = null;
        if (isEat)
        {
            word = GetRandomWord(eatWords, usedEatWords.ToArray());
            usedEatWords.Add(System.Array.IndexOf(eatWords, word));
        }
        else
        {
            word = GetRandomWord(notEatWords, usedNotEatWords.ToArray());
            usedNotEatWords.Add(System.Array.IndexOf(notEatWords, word));
        }
        var level = new ChooseController<WordSObject>.Level(word, new WordSObject[] { word, word }, isEat ? 0 : 1);
        return level;
    }

    public override string GetOption(WordSObject word)
    {
        throw new System.NotImplementedException();
    }

    public override string GetQuestionText(WordSObject word)
    {
        return word.emoji;
    }

    protected override WordSObject[] LoadItems()
    {
        eatWords = GetEatWords();
        notEatWords = GetNotEatWords();
        return WordBank.GetAllWords();
    }
    protected WordSObject[] GetEatWords()
    {
        return WordBank.GetWordsByCategory("food");
    }
    protected WordSObject[] GetNotEatWords()
    {
        return WordBank.GetWordsByCategory("thing");
    }
}
