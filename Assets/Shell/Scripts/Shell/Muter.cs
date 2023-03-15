using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Muter : MonoBehaviour
{
	private bool mute = false;
	[SerializeField] private bool music;
	[SerializeField] private AudioListener audioListener;
	[SerializeField] private Sprite muteSprite;
	[SerializeField] private Sprite unmuteSprite;
	[SerializeField] private Image im;

	public static bool musicMute = false;
	public static bool soundMute = false;

	public void Switch() {
		mute = !mute;
		if ( music ) musicMute = mute;
		else soundMute = mute;
		foreach (var aud in FindObjectsOfType<AudioSource>(true) ) {
			if (!(music ^ aud.playOnAwake)) aud.mute = mute;
		}
		im.sprite = mute ? muteSprite : unmuteSprite;
	}
}
