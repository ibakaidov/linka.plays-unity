using UnityEngine;

namespace Shell {
	public class ControlMaster : MonoBehaviour
	{
		[SerializeField] private ControlMode controlMode;

		public static ControlMode mode;

		private void Awake()
		{
			mode = controlMode;
		}
	}
}
