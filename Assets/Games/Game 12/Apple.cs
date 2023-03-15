using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shell;

namespace Game12 {

	public class Apple : MonoBehaviour {

		public static Apple mainApple;

		private void Awake() {
			mainApple = this;
		}
		public void OnTriggerEnter2D(Collider2D collision) {
			SnakeController.instance.AddScore();
			SnakeController.instance.SpawnApple();
			Destroy(gameObject);
		}

		private void Update() {
			SnakeController.instance.move =
				Vector2.Distance(Camera.main.WorldToScreenPoint(transform.position), SightMaster.Point) < 200;
		}
	}
}
