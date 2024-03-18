using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CaterogyFiller : MonoBehaviour
{

    public MenuController MenuController;
    public GameObject CategoryButtonPrefab;

    GameCategory[] categories;

    // Start is called before the first frame update
    void Start()
    {
        MenuController = FindObjectOfType<MenuController>();
        categories = Resources.LoadAll<GameCategory>("Games/Categories");

        foreach (var category in categories)
        {
            var button = Instantiate(CategoryButtonPrefab, transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = category.Name;
            button.GetComponent<RectTransform>().SetParent(transform);
            
            var finalCategory = category;
            button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                MenuController.SetCategory(finalCategory);
            });

        }
    }
}
