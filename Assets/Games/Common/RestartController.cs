using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartController : MonoBehaviour
{
    public GameObject GamePrefab;
    private GameObject instance;

    // Start is called before the first frame update

    private void OnEnable()
    {
        instance = GameObject.Instantiate(GamePrefab);
        RectTransform rect = instance.GetComponent<RectTransform>();
        
        rect.SetParent( GetComponent<RectTransform>());
        rect.SetAsFirstSibling();
        rect.localScale = Vector3.one;

        rect.offsetMin = Vector3.zero;
        rect.offsetMax = Vector3.zero;
    }
    private void OnDisable()
    {
        Destroy(instance);          
    }
}
