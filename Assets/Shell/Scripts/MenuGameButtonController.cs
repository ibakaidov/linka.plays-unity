using Shell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGameButtonController : MonoBehaviour
{
    internal GameWindow game;
    private Image icon;

    // Start is called before the first frame update
    void Start()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        icon.sprite = game.Icon;

        GetComponent<EyeButton>()
            .onClick.AddListener(() => { StartGame(); });
    }
    void StartGame()
    {
        MainMenu.instance.OpenGame(game);
    }
}
