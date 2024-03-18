using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class ChooseController<T> : MonoBehaviour
{
    T[] items;

    public T[] Items
    {
        get
        {
            if (items == null)
            {
                items = LoadItems();
            }
            return items;
        }
    }

    protected abstract T[] LoadItems();

    public abstract Level FillLevel();

    public abstract string GetQuestionText(T word);
    public string[] GetOptionsText(T[] word)
    {
        var options = new string[word.Length];

        for (int i = 0; i < word.Length; i++)
        {
            options[i] = GetOption(word[i]);
        }
        return options;
    }

    public abstract string GetOption(T word);

    protected T GetRandomWord(int[] skips)
    {
        var index = Random.Range(0, Items.Length);
        do {
            index = Random.Range(0, Items.Length);

        } while (System.Array.IndexOf(skips, index) != -1);
        return Items[index];
    }

    protected T GetRandomWord(T[] items, int[] skips){
        var index = Random.Range(0, items.Length);
        do {
            index = Random.Range(0, items.Length);

        } while (System.Array.IndexOf(skips, index) != -1);
        return items[index];
    }
    public class Level {
        
        public T question;
        public T[] options;

        public int correctIndex;
        public Level(T question, T[] options, int correctIndex)
        {
            this.question = question;
            this.options = options;
            this.correctIndex = correctIndex;
        }
    }
    public T[] Snuffle(T[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            var temp = items[i];
            var randomIndex = Random.Range(0, items.Length);
            items[i] = items[randomIndex];
            items[randomIndex] = temp;
        }
        return items;
    }
}
