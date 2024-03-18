using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    public Game Game;

    public Image GameIcon;
    public TMPro.TextMeshProUGUI GameName;

    // Start is called before the first frame update
    void Start()
    {
        GameIcon.sprite = Game.Icon;
        GameName.text = Game.Name;


    }
    public void OnClick()
    {
        SceneManager.LoadScene(Game.GameSceneId);

    }
}
