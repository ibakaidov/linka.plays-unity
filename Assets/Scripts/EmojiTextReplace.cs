using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmojiTextReplace : MonoBehaviour
{
    Image image;
    private Sprite[] emojis;

    // Start is called before the first frame update
    void Start()
    {
        var min = 100;
        //create gameobject with image
        var go = new GameObject("Emoji");
        image = go.AddComponent<Image>();
        image.transform.SetParent(transform);
        image.rectTransform.sizeDelta = new Vector2(min, min);
        image.rectTransform.anchoredPosition = new Vector2(0, 0);
        emojis =  Resources.LoadAll<Sprite>("emoji");
    }

    // Update is called once per frame
    void Update()
    {
        var text = GetComponent<TextMeshProUGUI>().text;
        int c = Char.ConvertToUtf32(text, 0);
        if (c<0x1000 || c>0x1F0000)
        {
            image.enabled = false;
            return;
        }
        
        string hex = c.ToString("X");
        
        Sprite emojiSprite = null;
        int minLen = int.MaxValue;
        foreach (var emoji in emojis)
        {
            if (emoji.name.ToLower().Contains(hex.ToLower()))
            {
                if (emoji.name.Length < minLen)
                {
                    minLen = emoji.name.Length;
                    emojiSprite = emoji;
                }
            }
        }
        if (emojiSprite != null)
        {
            image.sprite = emojiSprite;
            // GetComponent<TextMeshProUGUI>().enabled = false;
        }
        else
        {
            image.sprite = null;
            // GetComponent<TextMeshProUGUI>().enabled = true;
        }
        
    }
}
