using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shell {
	public class ControlSwitcher : MonoBehaviour {
		[SerializeField] private Sprite sightSprite;
		[SerializeField] private Sprite mouseSprite;
		[SerializeField] private Image im;

		public void Switch() {
			im.sprite = im.sprite == sightSprite ? mouseSprite : sightSprite;
			ControlMaster.mode = ControlMaster.mode != ControlMode.Sight ? ControlMode.Sight : ControlMode.Mouse;
		}
	}
}
