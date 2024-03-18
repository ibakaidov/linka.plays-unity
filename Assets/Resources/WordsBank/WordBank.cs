using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class WordBank
{

    public static WordSObject GetWord(string id)
    {
        return Resources.Load<WordSObject>($"Words/{id}");
    }
    public static WordSObject[] GetAllWords()
    {
        var guids = Resources.LoadAll<WordSObject>("WordsBank/Words");
        var words = new WordSObject[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            words[i] = guids[i];
        }
        return words;
    }
    public static string[] GetAllCategories()
    {
        var guids = Resources.LoadAll<WordSObject>("WordsBank/Words");
        var categories = new List<string>();
        for (int i = 0; i < guids.Length; i++)
        {
            if (!categories.Contains(guids[i].category))
            {
                categories.Add(guids[i].category);
            }
        }
        return categories.ToArray();
    }

    public static WordSObject[] GetWordsByCategory(string category)
    {
        var guids = Resources.LoadAll<WordSObject>("WordsBank/Words");
        var words = new List<WordSObject>();
        for (int i = 0; i < guids.Length; i++)
        {
            if (guids[i].category == category)
            {
                words.Add(guids[i]);
            }
        }
        return words.ToArray();
    }
    

    #if UNITY_EDITOR
    public static void CreateWordsFromCSV()
    {
        var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/WordsBank/words.csv");
        var lines = asset.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            var split = line.Split(',');
            if (split.Length != 4)
            {
                Debug.LogError($"Line {i} is not valid");
                continue;
            }
            CreateWord(split[0], split[1], split[2], split[3]);

        }

    }


    private static void CreateWord(string id, string word, string emoji, string category)
    {
        WordSObject o = ScriptableObject.CreateInstance<WordSObject>();
        o.word = word;
        o.emoji = emoji;
        o.category = category;
        TTS.SpeakAndSaveAudio(word, id);
        Thread.Sleep(1000);
        o.clip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>($"Assets/Audio/TTS_{id}.mp3");
        UnityEditor.AssetDatabase.CreateAsset(o, $"Assets/Resources/WordsBank/words/{id}.asset");
    }
    #endif
}
