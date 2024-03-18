using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeWordGameController : MonoBehaviour
{
    public WordSObject[] words;
    private string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
    private int current = 0;
    
    public FillTarget fillTarget;
    public FillKeyboard fillKeyboard;
    public TextMeshProUGUI emojiText;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        words = WordBank.GetAllWords();

        //sort by length
        words = words.OrderBy(w => w.word.Length).ToArray();

        FillLevel();
    }
    void FillLevel()
    {
        var word = words[current];
        fillTarget.Current = 0;
        fillTarget.Fill(word.word);
        emojiText.text = word.emoji;
        audioSource.clip = word.clip;
        audioSource.Play();
        string keys = word.word;
        
        while(keys.Length < 5)
        {
            char k = alphabet[Random.Range(0, alphabet.Length)];
            while(keys.IndexOf(k) != -1)
            {
                k = alphabet[Random.Range(0, alphabet.Length)];
            }
            keys += k;
        }
        keys = Shuffle(keys);
        fillKeyboard.Fill(keys);
        for (int i = 0; i < fillKeyboard.Prefabs.Length; i++)
        {
            int x = i;
            fillKeyboard.Prefabs[x]
                .GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    var key = keys[x];
                    if (word.word[fillTarget.Current] != key)
                    {
                        GameCounter.Instance.MarkMiss();
                    }
                    else
                    {
                       
                        if(fillTarget.Current == word.word.Length - 1)
                        {
                                GameCounter.Instance.Answer(true);

                            current++;
                            if (current == words.Length)
                            {
                                current = 0;
                            }
                            FillLevel();
                            fillTarget.Current = 0;

                            return;
                        }
                        else
                        {
                            fillTarget.Current++;

                        }
                    }
                });
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    static string Shuffle(string input)
    {
        var random = new System.Random();
        char[] characters = input.ToCharArray();

        // Fisher-Yates shuffle algorithm
        for (int i = characters.Length - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            char temp = characters[i];
            characters[i] = characters[j];
            characters[j] = temp;
        }

        return new string(characters);
    }
}
