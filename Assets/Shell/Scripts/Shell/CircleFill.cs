using UnityEngine;
using UnityEngine.UI;

namespace Shell {
	public class CircleFill : MonoBehaviour {
		[SerializeField] private Image image;

		public float Amount {
			set{
				image.fillAmount = value;
				Vector3 eu = image.transform.eulerAngles;
				eu.z = -180 * value;
				image.transform.eulerAngles = eu;
			}
		}
	}
}
