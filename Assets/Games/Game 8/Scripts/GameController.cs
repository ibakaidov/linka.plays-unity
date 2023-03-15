using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game8 {
	public class GameController : MonoBehaviour {
		[SerializeField] private GameObject[] levels;
		[SerializeField] public RectTransform buttonRT;
		[SerializeField] public Image previewImage;
		[SerializeField] public AspectRatioFitter arf;
		[SerializeField] public RectTransform effectImage;
		[SerializeField] public ParticleSystem effect;
		[SerializeField] private Transform placeForLevels;
		[SerializeField] private AudioSource winSound;
		[SerializeField] private GameObject winPanel;
		[SerializeField] private ParticleSystem winEffect;

		private ItemSwitcher itemSwitcher;
		private int current = 0;

		private void Clear() {
			if (placeForLevels.childCount > 0 ) {
				Destroy(placeForLevels.GetChild(0).gameObject);
			}
		}

		public void Next() {
			SetLevel(current + 1);
		}

		private void SetLevel(int i) {
			if (i >= levels.Length ) {
				i = 0;
			}
			current = i;
			Clear();
			itemSwitcher = Instantiate(levels[i], placeForLevels).GetComponentInChildren<ItemSwitcher>();
			itemSwitcher.GetComponentInParent<AudioSource>().mute = Muter.musicMute;
			itemSwitcher.gameController = this;
		}

		public void Click() {
			itemSwitcher.Click();
		}

		private void OnEnable() {
			SetLevel(0);
		}

		public void Win() {
			winSound.Play();
			StartCoroutine(WinCor());
		}

		private IEnumerator WinCor() {
			winPanel.SetActive(true);
			winEffect.Play();
			float time = winEffect.main.duration;
			yield return new WaitForSeconds(time / 2);
			SetLevel(current + 1);
			yield return new WaitForSeconds(time / 2);
			winPanel.SetActive(false);
		}
	}

	
}
