using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountItemsController : ChooseController<int>
{


    public GameObject items;
    public GameObject options;
    private int itemsCount{
        get{
            return items.transform.childCount;
        }
    }
    private int optionsCount{
        get{
            return options.transform.childCount;
        }
    }
    private List<int> selected;
    // Start is called before the first frame update
    void Start()
    {
        selected = new List<int>();
        FillUI();
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FillUI()
    {
        var level = FillLevel();
        Debug.Log(level.question);
        for (int i = 0; i < itemsCount; i++)
        {
            items.transform.GetChild(i).gameObject.SetActive(i < level.question);
        }
        for (int i = 0; i < optionsCount; i++)
        {
            options.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = level.options[i].ToString(); 
            options.transform.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
            var index = i;
            options.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log(index +":"+ level.correctIndex);
                if (index == level.correctIndex)
                {
                    Debug.Log("Correct");
                    GameCounter.Instance.Answer(true);
                }
                else
                {
                    GameCounter.Instance.Answer(false);
                    Debug.Log("Incorrect");
                }
                FillUI();
            }); 

            
        }
    }
    public override Level FillLevel()
    {
        var number = Random.Range(1, 10);   
        
        while (selected.Contains(number))
        {
            number = Random.Range(1, 10);
        }

        selected.Add(number);

        var answers = new List<int>();
        answers.Add(number);
        for (int i = 1; i < optionsCount; i++)
        {
            var answer = Random.Range(1, 10);
            while (answers.Contains(answer))
            {
                answer = Random.Range(1, 10);
            }
            answers.Add(answer);
        }
        var answersArray = answers.ToArray();
        answersArray = Snuffle(answersArray);
        int answerIndex = 0;
        for (int i = 0; i < answersArray.Length; i++)
        {
            if (answersArray[i] == number)
            {
                answerIndex = i;
                break;
            }
        }
        var level = new Level(number, answersArray, answerIndex);
        return level;
    }

    public override string GetOption(int word)
    {
        throw new System.NotImplementedException();
    }

    public override string GetQuestionText(int word)
    {
        throw new System.NotImplementedException();
    }

    protected override int[] LoadItems()
    {
        throw new System.NotImplementedException();
    }

}
