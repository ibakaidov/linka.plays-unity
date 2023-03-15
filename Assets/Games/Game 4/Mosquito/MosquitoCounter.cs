using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MosquitoCounter : MonoBehaviour
{
    public static int counter;
    Text textComponent;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.TryGetComponent<Text>(out textComponent);
    }

	private void OnEnable() {
		counter = 0;
	}

	// Update is called once per frame
	void Update()
    {
        textComponent.text = $"Поймано комаров: {counter}";
    }
}
