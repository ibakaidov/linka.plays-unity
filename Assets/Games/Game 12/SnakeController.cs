using Shell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game12 {
	public class SnakeController : MonoBehaviour {
		[SerializeField] private LineRenderer line;
		[SerializeField] private Transform head;
		[SerializeField] private float speed = 5;
		[SerializeField] private float startLength = 1;
		[SerializeField] private GameObject applePrefab;
		[SerializeField] private GameObject effectPrefab;
		[SerializeField] private TMP_Text text;
		[SerializeField] private SpriteRenderer headSR;
		[SerializeField] private Sprite closedSprite;
		[SerializeField] private Sprite openedSprite;
		[SerializeField] private RectTransform screenRT;

		[HideInInspector] public bool move = false;

		private int score = 0;
		private Vector2 applePosition = Vector2.zero;

		public static SnakeController instance;

		private void Awake() {
			instance = this;
		}

		private Vector2 headPosition;
		private Vector2 direction = Vector2.right;

		private List<Vector2> points = new List<Vector2>() { Vector2.zero };

		public void SpawnApple() {
			var se = EdgeHelper.GetEdge();
			applePosition = se.RandomPoint(0.5f, 3f);

			/*Vector3[] cs = new Vector3[4];
			screenRT.GetWorldCorners(cs);

			applePosition.x *= 1920f / cs[2].x;
			applePosition.y *= 1080f / cs[2].y;*/

			Instantiate(instance.applePrefab, applePosition, Quaternion.identity).transform.parent = transform;
		}

		public void EatApple() {
			Apple.mainApple?.OnTriggerEnter2D(null);
		}

		private void OnEnable() {
			float deltaD = 0.02f;
			int n = (int)(startLength / deltaD);
			points.Clear();
			points.Add(Vector2.zero);
			headPosition = Vector2.zero;
			direction = Vector2.right;
			for (int i = 1; i < n; i++ ) {
				points.Add(Vector2.left * deltaD * i);
			}
			foreach ( Apple apple in FindObjectsOfType<Apple>(true) ) Destroy(apple.gameObject);
			SpawnApple();
			Render();
		}

		public void AddScore() {
			Vector2 dir = (points[points.Count - 1] - points[points.Count - 2]).normalized;
			Vector2 p = points[points.Count - 1];
			float dl = 1;
			float ddl = 0.03f;

			Instantiate(effectPrefab, applePosition, Quaternion.identity).transform.parent = transform;


			int n = (int)(dl / ddl);
			for ( int i = 1; i < n; i++ ) {
				points.Add(p + dir * dl * (float)i / n);
			}

			score++;
			text.text = score.ToString();
		}

		private void Update() {
			if ( move ) {
				Move();
				Render();
			}
		}

		public void Move() {
			var se = EdgeHelper.GetEdge();

			Vector2 target = Apple.mainApple.transform.position;
			Vector2 d = (target - headPosition).normalized;
			float angle = -Vector2.SignedAngle(direction, d);
			
			float rs = 100f;
			angle = Mathf.Clamp(angle, -rs * Time.deltaTime, rs * Time.deltaTime);
			direction = direction * Mathf.Cos(Mathf.PI / 180f * angle) + new Vector2(direction.y, -direction.x) * Mathf.Sin(Mathf.PI / 180f * angle);
			headPosition += direction * speed * Time.deltaTime;

			if ( se.NeedToInvert(headPosition) ) {
				headPosition = se.Clamp(headPosition);
			}

			AddPoint(headPosition);
		}

		public void Render() {
			headSR.sprite = Vector2.Distance(headPosition, applePosition) > 1.5f ? closedSprite : openedSprite;

			line.positionCount = points.Count;
			for ( int i = 0; i < points.Count; i++ ) {
				line.SetPosition(i, points[i]);
			}
			head.position = headPosition;
			head.right = direction;
		}

		public void AddPoint(Vector2 point) {
			if ( points.Count < 3 ) {
				points.Insert(0, point);
				return;
			}
			if ( point == points[0] ) return;
			float distance = Vector2.Distance(point, points[0]);
			float d = 0;
			int i = points.Count - 1;
			while ( d < distance && i > 0 ) {
				float dist = Vector2.Distance(points[i], points[i - 1]);
				d += dist;
				if (d < distance ) {
					//i--;
					points.RemoveAt(i);
					i--;
				}
			}
			float dd = d - distance;
			if (i > 0) points[i] = points[i - 1] + (points[i] - points[i - 1]).normalized * dd;
			points.Insert(0, point);
		}
	}
}
