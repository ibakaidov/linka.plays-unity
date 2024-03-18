using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject CategoriesList;
    public GameObject GamesList;
    GameCategory _currentCategory;
    GameCategory CurrentCategory
    {
        get
        {
            return _currentCategory;
        }
        set
        {
            _currentCategory = value;

            CategoriesList.SetActive(!state);
            GamesList.SetActive(state);
            GamesList.GetComponent<GamesFiller>().Category = value;
        }
    }
    bool state
    {
        get
        {
            return CurrentCategory != null;
        }
    }


    public void SetCategory(GameCategory category)
    {

        CurrentCategory = category;
    }

    public void Back()
    {
        if (state)
        {
            CurrentCategory = null;
        }
        else
        {
            Application.Quit();
        }
    }
}
