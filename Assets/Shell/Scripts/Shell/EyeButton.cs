using Tobii.Gaming;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Shell
{
	[AddComponentMenu("Eye Button")]
	public class EyeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] private UnityEvent onBegin;
		[SerializeField] private UnityEvent onEnd;
		[SerializeField] private UnityEvent<float> ratioEvent;
		[SerializeField] internal UnityEvent onClick;
		[SerializeField] private UnityEvent onFail;
		[SerializeField] public float timeScale = 1;

		private float t = 0;
		private bool seen = false;
		private bool lastSeen = false;

		public void OnPointerEnter(PointerEventData eventData) {
			onBegin.Invoke();
			seen = true;
		}

		public void OnPointerExit(PointerEventData eventData) {
			onEnd.Invoke();
			seen = false;
		}
	
		void Start()
		{
			gameObject.layer = LayerMask.NameToLayer("EyeButton");
		}
		private void Update() {


			
            if (Input.GetMouseButtonDown(0))
            {
                
				if(RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition))
				{
					onClick?.Invoke();
				}

            }
			

            switch ( ControlMaster.mode ) {
				case ControlMode.Mouse:
					Control(seen);
					break;
				case ControlMode.Sight:
					Control(SightMaster.focusedUI == gameObject);
					break;
				default:
					Control(SightMaster.focusedUI == gameObject || seen);
					break;
			}
			lastSeen = seen;
		}

		private void OnEnable() {
			seen = false;
		}

		private void Control(bool seen) {
			if ( seen ) {
				if ( t < Settings.time ) {
					t += Time.deltaTime * timeScale;
				}
				else {
					onClick.Invoke();
					t = 0;
				}
			}
			else {
				if ( t > 0 ) {
					if ( lastSeen ) {
						onFail.Invoke();
					}
					t -= 5 * Time.deltaTime * timeScale;
				}
				else {
					t = 0;
				}
			}
			ratioEvent.Invoke(t / Settings.time);
		}
	}
}
