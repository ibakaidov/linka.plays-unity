using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game3 {
	public class Window : MonoBehaviour {
		[SerializeField] private Animator animator;
		[SerializeField] private Sprite openedSprite;
		[SerializeField] private Sprite closedSprite;
		[SerializeField] private Image image;
		[SerializeField] private Image bg;
		[SerializeField] private Color[] colors;
		[SerializeField] private GameObject insidePart;
		[SerializeField] private Transform placeForAnimal;
		[SerializeField] private RectTransform placeForLoad;

		public Vector2 ButtonPlace {
			get {
				Vector3[] cs = new Vector3[4];
				placeForLoad.GetWorldCorners(cs);
				return (cs[0] + cs[2]) / 2;
			}
		}

		private void Awake() {
			if ( colors.Length > 0 ) {
				bg.color = colors[Random.Range(0, colors.Length)];
			}
		}

		public void SetAnimal(GameObject prefab) {

			var children = new List<GameObject>();
			foreach ( Transform child in placeForAnimal ) children.Add(child.gameObject);
			children.ForEach(child => Destroy(child));

			if ( insidePart ) {
				image.sprite = openedSprite;
				insidePart.SetActive(true);
			}

			GameObject animal = Instantiate(prefab, placeForAnimal);
			animator = animal.GetComponent<Animator>();
		}

		public bool IsDoor {
			get => insidePart;
		}

		public void Feed() {
			var children = new List<GameObject>();
			foreach ( Transform child in placeForAnimal ) children.Add(child.gameObject);
			children.ForEach(child => Destroy(child));

			if ( IsDoor ) {
				image.sprite = closedSprite;
				insidePart.SetActive(false);
			}
		}
	}
}
