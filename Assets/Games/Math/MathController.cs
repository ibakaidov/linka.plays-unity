using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MathController : ChooseController<string>
{

    public TextMeshProUGUI QuestionText;
    private Level level;
    private string answer = "";
    private string questionContent
    {
        get
        {
            return level.question + " = " + answer;
        }
    }

    private bool additionAction = true;
    private bool subtractionAction = false;
    private bool multiplicationAction = false;
    private bool divisionAction = false;

    private bool digit_0 = true;
    private bool digit_1 = false;
    private bool digit_2 = false;
    private bool digit_3 = false;

    public bool AdditionAction
    {
        get
        {
            return additionAction;
        }
        set
        {
            additionAction = value;
        }
    }
    public bool SubtractionAction
    {
        get
        {
            return subtractionAction;
        }
        set
        {
            subtractionAction = value;
        }
    }
    public bool MultiplicationAction
    {
        get
        {
            return multiplicationAction;
        }
        set
        {
            multiplicationAction = value;
        }
    }
    public bool DivisionAction
    {
        get
        {
            return divisionAction;
        }
        set
        {
            divisionAction = value;
        }
    }
    public bool[] Actions
    {
        get
        {
            return new bool[] { additionAction, subtractionAction, multiplicationAction, divisionAction };
        }
    }
    public bool Digit_0
    {
        get
        {
            return digit_0;
        }
        set
        {
            digit_0 = value;
        }
    }
    public bool Digit_1
    {
        get
        {
            return digit_1;
        }
        set
        {
            digit_1 = value;
        }
    }
    public bool Digit_2
    {
        get
        {
            return digit_2;
        }
        set
        {
            digit_2 = value;
        }
    }
    public bool Digit_3
    {
        get
        {
            return digit_3;
        }
        set
        {
            digit_3 = value;
        }
    }

    void Start()
    {
        FillUI();
    }

    void FillUI()
    {
        level = FillLevel();
        answer = "";
        QuestionText.text = questionContent;
    }

    public override Level FillLevel()
    {
        var action = GetRandomAction();
        var digit = GetRandomDigit();
        digit++;
        Debug.Log("Action: " + action + " Digit: " + digit);
        if (action == 0)
        {
            var target = UnityEngine.Random.Range(2, (int)Math.Pow(10, digit));
            var a = UnityEngine.Random.Range(1, target - 1);
            var b = target - a;
            return new Level(a + " + " + b, new string[] { (a + b).ToString(), (a + b + 1).ToString(), (a + b - 1).ToString(), (a + b + 2).ToString() }, 0);

        }
        else if (action == 1)
        {
            var a = UnityEngine.Random.Range(2, (int)Math.Pow(10, digit));
            var b = UnityEngine.Random.Range(1, a);
            return new Level(a + " - " + b, new string[] { (a - b).ToString(), (a - b + 1).ToString(), (a - b - 1).ToString(), (a - b + 2).ToString() }, 0);
        }
        else if (action == 2)
        {
            var a = UnityEngine.Random.Range(1, (int)Math.Pow(10, digit));
            var b = UnityEngine.Random.Range(1, (int)Math.Pow(10, digit));
            return new Level(a + " * " + b, new string[] { (a * b).ToString(), (a * b + 1).ToString(), (a * b - 1).ToString(), (a * b + 2).ToString() }, 0);
        }
        else if (action == 3)
        {
            var a = UnityEngine.Random.Range(1, (int)Math.Pow(10, digit));
            var b = UnityEngine.Random.Range(1, a);
            var c = a * b;
            return new Level(c + " / " + a, new string[] { b.ToString(), (b + 1).ToString(), (b - 1).ToString(), (b + 2).ToString() }, 0);
        }
        throw new Exception("No action enabled");
    }

    private int GetRandomAction()
    {
        List<int> enabledActions = new List<int>();
        for (int i = 0; i < Actions.Length; i++)
        {
            if (Actions[i])
            {
                enabledActions.Add(i);
            }
        }
        return enabledActions[UnityEngine.Random.Range(0, enabledActions.Count)];
    }
    private int GetRandomDigit()
    {
        List<int> enabledDigits = new List<int>();
        if (Digit_0)
        {
            enabledDigits.Add(0);
        }
        if (Digit_1)
        {
            enabledDigits.Add(1);
        }
        if (Digit_2)
        {
            enabledDigits.Add(2);
        }
        if (Digit_3)
        {
            enabledDigits.Add(3);
        }
        return enabledDigits[UnityEngine.Random.Range(0, enabledDigits.Count)];
    }

    public override string GetOption(string word)
    {
        return word;
    }

    public override string GetQuestionText(string word)
    {
        return word;
    }

    public void TypeNumber(int number)
    {
        answer += number;
        QuestionText.text = questionContent;
    }
    public void CheckAnswer()
    {
        if (answer == level.options[level.correctIndex])
        {
            FillUI();
            GameCounter.Instance.Answer(true);
        } else
        {
            answer = "";
            QuestionText.text = questionContent;
            GameCounter.Instance.MarkMiss();
        }
    }
    public void Backspace()
    {
        if (answer.Length > 0)
        {
            answer = answer.Substring(0, answer.Length - 1);
            QuestionText.text = questionContent;
        }
    }
    protected override string[] LoadItems()
    {
        throw new System.NotImplementedException();
    }
}
