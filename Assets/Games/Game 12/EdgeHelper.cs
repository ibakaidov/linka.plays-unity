using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game12 {
	public struct ScreenEdge {
		public Vector2 e;

		public ScreenEdge(Vector2 e) {
			this.e = e;
		}

		public Vector2 Inverse(Vector2 point) {
			for ( int i = 0; i < 2; i++ ) {
				if ( point[i] < -e[i] ) point[i] += 2 * e[i];
				else if ( point[i] > e[i] ) point[i] -= 2 * e[i];
			}
			return point;
		}

		public Vector2 RandomPoint(float border, float v) {
			float x = Random.Range(-e.x + border, e.x - border);
			float y = Random.Range(-e.y + border, e.y - border);
			while (x < -e.x + border + v && y < -e.y + border + v ) {
				x = Random.Range(-e.x + border, e.x - border);
				y = Random.Range(-e.y + border, e.y - border);
			}
			return new Vector2(x, y);
		}

		public Vector2 Clamp(Vector2 point) {
			return new Vector2(Mathf.Clamp(point.x, -e.x, e.x), Mathf.Clamp(point.y, -e.y, e.y));
		}

		public bool NeedToInvert(Vector2 point) {
			for ( int i = 0; i < 2; i++ ) {
				if ( point[i] < -e[i] ) return true;
				else if ( point[i] > e[i] ) return true;
			}
			return false;
		}
	}

	public static class EdgeHelper {
		public static ScreenEdge GetEdge() {
			return new ScreenEdge(Camera.main.ViewportToWorldPoint(Vector2.one));
		}
	}
}
