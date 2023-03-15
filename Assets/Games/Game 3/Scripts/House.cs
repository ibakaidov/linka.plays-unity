using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

namespace Game3 {
	public class House : MonoBehaviour {
		[SerializeField] private GameObject windowPrefab;
		[SerializeField] private Window doorWindow;
		[SerializeField] private Transform placeForWindows;
		[SerializeField] private Animal[] animals;
		[SerializeField] private Shell.EyeButton button;
		[SerializeField] private Animator goalAnim;
		[SerializeField] private TMP_Text goalText;
		[SerializeField] private ParticleSystem winEffect;
		[SerializeField] private ParticleSystem heartEffect;
		[SerializeField] private RectTransform heartRect;
		[SerializeField] private AudioSource winAS;
		[SerializeField] private List<int> levels;
		[SerializeField] private RectTransform screenRT;

		[SerializeField] private Image wood;
		[SerializeField] private Image door;

		[SerializeField] private int level = 0;

		[Header("Sound")]
		[SerializeField] private AudioSource appearAS;
		[SerializeField] private AudioSource successAS;
		[SerializeField] private AudioSource failureAS;

		[SerializeField] private TMP_Text ratioText;
		[SerializeField] private Slider slider;
		private float startRatio = 1;
		[SerializeField] private Shell.EyeButton eyeButton;

		private void Awake() {
			startRatio = eyeButton.timeScale;
		}

		public float SliderChanged {
			set {
				float r = Mathf.Pow(5, slider.value);
				float ratio = startRatio * r;
				eyeButton.timeScale = ratio;
				ratioText.text = r.ToString();
			}
		}

		private float maxAppearTime = 2f;
		private float appearTime = 0;
		private float time = 0;
		private bool needToSpawn = true;
		private int animalsCount = 0;

		private List<Window> windows = new List<Window>();
		private int choosenWindow = 0;
		bool pause = false;
		float pausePeriod = 2;
		float pauseTime = 0;

		public void AddLevel() {
			level++;
			animalsCount = 0;
			goalAnim.PlayInFixedTime("GoalPanel", 0, 0);
			winEffect.Play();
			winAS.Play();
			pause = true;
			SetLevel(level);
		}

		private int Columns {
			get {
				int columns = (int)Mathf.Sqrt(
						(level - 1) < levels.Count ? levels[level - 1] : (level - levels.Count)
					);
				if ( columns < 3 ) {
					columns = 3;
				}
				if ( columns % 2 == 0 ) {
					columns--;
				}
				return columns;
			}
		}

		private void Update() {
			if ( pause ) {
				Time.timeScale = 0f;
				pauseTime += Time.unscaledDeltaTime;
				if ( pauseTime > pausePeriod ) {
					pauseTime = 0;
					pause = false;
					Time.timeScale = 1f;
				}
			}
		}

		private void OnEnable() {
			Time.timeScale = 1f;
			SetLevel(1);
		}

		public void SpawnAnimal() {
			int i = Random.Range(0, animals.Length);
			int wi = Random.Range(0, windows.Count);
			while (wi == choosenWindow && windows.Count > 1 ) {
				wi = Random.Range(0, windows.Count);
			}
			Debug.Log(i + " " + wi + " - " + "Spawn");
			if ( windows[wi].IsDoor ) {
				windows[wi].SetAnimal(animals[i].doorVersion);
			}
			else {
				windows[wi].SetAnimal(animals[i].windowVersion);
			}
			choosenWindow = wi;

			Vector3[] cs = new Vector3[4];
			screenRT.GetWorldCorners(cs);

			Vector2 p = windows[wi].ButtonPlace;

			p.x *= 1920f / cs[2].x;
			p.y *= 1080f / cs[2].y;

			button.GetComponent<RectTransform>().anchoredPosition = p;
			appearAS.clip = animals[i].appearSound;
			appearAS.Play();
		}

		public float xCor(int cs, int i) {
			float add = 1600f / (cs + 1);
			return -800 + (i + 1) * add;
		}

		public float yCor(int i, int height, float size) {
			float add = 800f / (height+ 1);
			return (i + 1.15f) * add;
		}

		public void Success() {
			windows[choosenWindow].Feed();
			animalsCount++;
			int count = (level - 1) < levels.Count ? levels[level - 1] : (level - levels.Count);
			goalText.text = (count - animalsCount).ToString();
			heartRect.anchoredPosition = windows[choosenWindow].ButtonPlace;
			heartEffect.Play();
			if (animalsCount >= count ) {
				AddLevel();
			}
			else {
				SpawnAnimal();
				successAS.Play();
			}
		}

		public void Fail() {
			failureAS.Play();
		}

		public void SetLevel(int level) {

			var children = new List<GameObject>();
			foreach ( Transform child in placeForWindows ) children.Add(child.gameObject);
			children.ForEach(child => Destroy(child));

			this.level = level;
			int cs = Columns;
			int count = (level - 1) < levels.Count ? levels[level - 1] : (level - levels.Count);

			goalText.text = count.ToString();

			int height = Mathf.CeilToInt((float)count / cs);
			float size = 2f / Mathf.Sqrt(cs * cs + height * height);
			windows.Clear();

			float sizeX = 1600f / (cs + 1);
			float sizeY = 800f / (height + 1);

			size = Mathf.Min(sizeX / 600, sizeY / 450);

			wood.pixelsPerUnitMultiplier = 0.33f / size;
			door.transform.localScale = Vector3.one * size;

			int c = 0;
			for (int i = 0; i < height; i++ ) {
				for (int j = 0; j < cs; j++ ) {
					if (c > count - 1 ) {
						break;
					}
					if (j == cs / 2 && i == 0 ) {
						windows.Add(doorWindow);
					}
					else {
						float x = xCor(cs, j);
						float y = yCor(i, height, size);
						Window window = Instantiate(windowPrefab, placeForWindows).GetComponent<Window>();
						window.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
						windows.Add(window);
					}
					c++;
				}
			}

			Debug.Log("size: " + windows.Count);

			foreach (var window in windows ) {
				window.transform.localScale = Vector3.one * size;
			}

			SpawnAnimal();
		}
	}
}
