using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


public class TTS {
     private static string apiUrl = "https://tts.linka.su/tts";
    private static string audioSavePath = "Assets/Audio/";


    public static void SpeakAndSaveAudio(string textToSpeak, string fileId)
    {
        if (string.IsNullOrEmpty(textToSpeak))
        {
            Debug.LogError("Text to speak is empty!");
            return;
        }

        string requestUrl = $"{apiUrl}?text={UnityWebRequest.EscapeURL(textToSpeak)}";

        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        var operation = request.SendWebRequest();


        operation.completed += (operation) =>
            {
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error requesting TTS: " + request.error);
                }
                else
                {
                    byte[] audioData = request.downloadHandler.data;
                    SaveAudioClip(audioData, fileId);
                    Debug.Log("Text successfully spoken and audio saved!");
                }
            };
    }

    private static void SaveAudioClip(byte[] audioData, string fileId)
    {
        string fileName = $"TTS_{fileId}.mp3";
        string fullPath = Path.Combine(audioSavePath, fileName);

        if (!Directory.Exists(audioSavePath))
        {
            Directory.CreateDirectory(audioSavePath);
        }

        File.WriteAllBytes(fullPath, audioData);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
        Debug.Log($"Audio clip saved at: {fullPath}");
    }
}
