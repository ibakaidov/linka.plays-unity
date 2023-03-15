using UnityEngine;
using Tobii.Gaming;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Shell {
	[AddComponentMenu("Sight Master")]
	public class SightMaster : MonoBehaviour {
		public static GameObject focusedUI;
		public static GameObject focusedGO;

		[SerializeField] private bool uiOverlaping = true;

		private void Update() {
			//print(focusedGO + " " + focusedUI);
			//if (ControlMaster.mode != ControlMode.Mouse ) {
				GetFocusedUI();
				GetFocusedGO();
			//}
		}

		private void Start() {
			TobiiAPI.Start(new TobiiSettings());
		}

		private void GetFocusedUI() {
			PointerEventData pointerData = new PointerEventData(EventSystem.current);
			pointerData.position = Point;
			List<RaycastResult> results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerData, results);
			
			if ( results.Count > 0 ) {
				focusedUI = results[0].gameObject;
			}
			else {
				focusedUI = null;
			}
		}

		private void GetFocusedGO() {
			if ( uiOverlaping && focusedUI ) {
				focusedGO = null;
				return;
			}
			if (float.IsNaN(Point.x) || float.IsNaN(Point.y) ) {
				return;
			}
			Ray ray = Camera.main.ScreenPointToRay(Point);
			if ( Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit) ) {
				focusedGO = hit.collider.gameObject;
			}
			else {
				focusedGO = null;
			}
		}

		public static Vector2 Point {
			get {
				if (ControlMaster.mode == ControlMode.Mouse ) {
					return Input.mousePosition;
				}
				else {
					if ( TobiiAPI.IsConnected ) {
						return TobiiAPI.GetGazePoint().Screen;
					}
					else {
						return Input.mousePosition;
					}
				}
			}
		}
	}
}
