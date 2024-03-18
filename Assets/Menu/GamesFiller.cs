using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesFiller : MonoBehaviour
{
    private GameCategory _category;
    public GameObject GameButtonPrefab;
    public GameCategory Category
    {
        get
        {
            return _category;
        }
        set
        {
            _category = value;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            FillGames();
        }
    }
    Game[] games;


    // FillGames is called before the first frame update
    void FillGames()
    {
        games = Resources.LoadAll<Game>("Games/Games/");

        foreach (var game in games)
        {
            if (game.Category != Category)
            {
                continue;
            }
            var button = Instantiate(GameButtonPrefab, transform);
            button.GetComponent<GameButton>().Game = game;
            button.GetComponent<RectTransform>().SetParent(transform);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
