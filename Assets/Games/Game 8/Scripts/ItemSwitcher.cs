using Shell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game8 {
	public class ItemSwitcher : MonoBehaviour {
		private int current = 0;

		[HideInInspector] public GameController gameController;

		private List<Sprite> sprites = new List<Sprite>();
		private List<Vector2> points = new List<Vector2>();
		private List<float> ratios = new List<float>();
		private List<GameObject> gos = new List<GameObject>();
		private void Awake() {
			foreach ( Transform child in transform ) {
				var rt = child.GetComponent<RectTransform>();
				var im = child.GetComponent<Image>();
				points.Add(rt.anchoredPosition);
				sprites.Add(im.sprite);
				ratios.Add(rt.sizeDelta.x / rt.sizeDelta.y);
				gos.Add(child.gameObject);
			}
		}

		private void Start() {
			SetItem(0);
		}

		private void SetItem(int i) {
			current = i;
			gameController.buttonRT.anchoredPosition = points[i];
			gameController.previewImage.sprite = sprites[i];
			gameController.arf.aspectRatio = ratios[i];
		}

		public void Click() {
			gos[current].SetActive(false);
			gameController.effectImage.anchoredPosition = points[current];
			gameController.effect.Play();
			int next = current + 1;
			if ( next < sprites.Count ) {
				SetItem(next);
			}
			else {
				gameController.Win();
			}
		}
	}
}
