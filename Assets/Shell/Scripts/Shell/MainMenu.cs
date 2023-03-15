using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shell {
	[System.Serializable]
	public class Window {
		[SerializeField] private GameObject go;
		[SerializeField] private GameObject ui;
		public GameObject GO {
			get => go;
		}

		public GameObject UI {
			get => ui;
		}

		public void SetActive(bool active) {
			if (go != null)
			{
				go?.SetActive(active);	
			}
			if (ui != null)
			{
				ui?.SetActive(active);
			}
		}
	}
	[System.Serializable]
	public class GameWindow : Window
	{

        [SerializeField] private string title;
        [SerializeField] private int type;
        [SerializeField] private Sprite sprite;
		[SerializeField] private AudioClip instruction;
		[SerializeField] private int sceneId = -1;	
        public string Title
        {
            get => title;
        }
        public int Type { get => type; }
        public Sprite Icon { get => sprite; }
		public AudioClip Instruction { get => instruction; }
		public int SceneId { get => sceneId; }
    }
    public class MainMenu : MonoBehaviour {
		[SerializeField] private bool startFromMainMenu = true;
		[SerializeField] private Window menuWindow;
		[SerializeField] private GameWindow[] gameWindows;
		[SerializeField] private RectTransform Menu;
		[SerializeField] private GameObject GameButton;

        [SerializeField] private GameObject exitButton;
	

        public static MainMenu instance;
        private AudioSource mAudioSource;

        public void Quit() {
			Application.Quit();
		}

		private void Awake() {
			instance = this;
		}

		private void Start() {
			CreateMenu();
			mAudioSource = GetComponent<AudioSource>();
			if ( startFromMainMenu ) {
				ToMainMenu();
			}
		}

		private void CreateMenu()
		{
			for ( int i = 0; i < gameWindows.Length; i++ )
			{
				var gb = GameObject.Instantiate(GameButton);
				gb.GetComponent<MenuGameButtonController>().game = gameWindows[i];
				gb.GetComponent<RectTransform>().SetParent(Menu);
				gb.GetComponent<RectTransform>().localScale = Vector3.one;
			}
		}
		
        public void OpenGame(GameWindow window)
        {
			if (window.SceneId > 0)
			{
				OpenGameFromScene(window.SceneId);
				return;
			}
            menuWindow.SetActive(false);
            CloseGames();
			exitButton?.SetActive(true);
            window.SetActive(true);
			if(window.Instruction!=null)
			{
			mAudioSource.clip = window.Instruction;
				mAudioSource.Play();
			}
        }

        private void CloseGames() {
			for (int i = 0; i < gameWindows.Length; i++ ) {
				gameWindows[i]?.SetActive(false);
			}
			exitButton?.SetActive(false);
		}

		public void ToMainMenu() {
			CloseGames();
			exitButton.SetActive(false);
			menuWindow.SetActive(true);
			Settings.time = 1f; //to default for avoiding bugs
		}

		private void Update() {
			if ( Input.GetKeyDown(KeyCode.Escape) ) {
				ToMainMenu();
			}
		}

		public void OpenGameFromScene(int sceneId)
		{
			SceneManager.LoadScene(sceneId);
		}
	}
}
