using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Shell {
	[AddComponentMenu("Eye Button GO")]
	public class EyeButtonGO : MonoBehaviour {
		[SerializeField] private UnityEvent onBegin;
		[SerializeField] private UnityEvent onEnd;
		[SerializeField] private UnityEvent<float> ratioEvent;
		[SerializeField] private UnityEvent onClick;

		private float t = 0;
		private bool seen = false;
		private bool lastSeen = false;

		private void OnMouseEnter() {
			seen = true;
		}

		private void OnMouseExit() {
			seen = false;
		}
        private void Start()
        {
			gameObject.layer = LayerMask.NameToLayer("EyeButton");

        }

        private void Update() {
			switch ( ControlMaster.mode ) {
				case ControlMode.Mouse:
					Control(seen);
					break;
				case ControlMode.Sight:
					Control(SightMaster.focusedGO == gameObject);
					break;
				default:
					Control(SightMaster.focusedGO == gameObject || seen);
					break;
			}
		}

		private void OnEnable() {
			seen = false;
		}

		private void Control(bool seen) {
			if ( seen ) {
				if ( t < Settings.time ) {
					t += Time.deltaTime;
					ratioEvent.Invoke(t / Settings.time );
				}
				else {
					onClick.Invoke();
					t = 0;
				}
				if ( !lastSeen ) onBegin.Invoke();
			}
			else {
				if ( t > 0 ) {
					t -= 5 * Time.deltaTime;
				}
				else {
					t = 0;
				}
				if ( lastSeen ) onEnd.Invoke();
			}
			
			lastSeen = seen;
		}
	}
}

