using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Shell {
	[AddComponentMenu("Eye Cursor")]
	public class EyeCursor : MonoBehaviour {

		private RectTransform rt;
		[SerializeField] private Canvas _canvas;

		private void Awake() {
			rt = GetComponent<RectTransform>();
		}
		private void Update() {
			if ( rt ) {
				rt.anchoredPosition = SightMaster.Point / _canvas.scaleFactor;
			}
		}
	}
}
