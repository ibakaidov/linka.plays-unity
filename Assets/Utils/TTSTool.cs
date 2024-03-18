#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class TextToSpeechTool : EditorWindow {
    private string textToSpeak;
    private string fileId;

    [MenuItem("Custom Tools/Text to Speech")]
    static void Init()
    {
        TextToSpeechTool window = (TextToSpeechTool)EditorWindow.GetWindow(typeof(TextToSpeechTool));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Text to Speech Tool", EditorStyles.boldLabel);

        textToSpeak = EditorGUILayout.TextField("Enter Text:", textToSpeak);
        fileId = EditorGUILayout.TextField("Enter file id:", fileId);
        if (GUILayout.Button("Speak and Save Audio"))
        {
            TTS.SpeakAndSaveAudio(textToSpeak, fileId);
        }

        if (GUILayout.Button("Make WordsBank Assets"))
        {
            
            WordBank.CreateWordsFromCSV();
        }
    }
}

#endif