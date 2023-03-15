using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinScaler : MonoBehaviour
{
	[SerializeField] private float magnitude;
	[SerializeField] private float period;

	private float time;
	private Vector3 scale;

	private void Start() {
		time = period * Random.value;
		scale = transform.localScale;
	}

	private void Update() {
		time += Time.deltaTime;
		if ( time > period ) time = 0;
		transform.localScale = scale * (1 + magnitude * Mathf.Sin(2 * Mathf.PI * time / period));
	}
}
