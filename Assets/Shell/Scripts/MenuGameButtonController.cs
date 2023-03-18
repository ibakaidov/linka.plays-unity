using Shell;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuGameButtonController : MonoBehaviour
{
    internal GameWindow game;
    private Image icon;
    private TextMeshProUGUI number;

    // Start is called before the first frame update
    void Start()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        number = transform.Find("Number").GetComponent<TextMeshProUGUI>();
        icon.sprite = game.Icon;
        number.text = game.Index.ToString();

        GetComponent<EyeButton>()
            .onClick.AddListener(() => { StartGame(); });
    }
    void StartGame()
    {
        MainMenu.instance.OpenGame(game);
    }
}
