using UnityEngine;
using Game6.UI;
using Shell;

namespace Game6
{
    public class UIMediator : MonoBehaviour
    {
        [SerializeField] private Scoreboard _scoreboard;
        [SerializeField] private RectTransform _containerForConfigurations;
        [SerializeField] private EyeTrackerHandler _eyeTrackerHandler;

        private static UIMediator _instance;

        public static Scoreboard Scoreboard => _instance._scoreboard;
        public static RectTransform ContainerForConfigurations => _instance._containerForConfigurations;

        public static void RegisterTrackerButton(IEyeUpdater eyeUpdater, Vector2 position)
        {
            ChangeTrackerRingPosition(position);
            _instance._eyeTrackerHandler.AddEyeTracker(eyeUpdater);
        }

        public static void UnregisterTrackerButton(IEyeUpdater eyeUpdater) =>
            _instance._eyeTrackerHandler.RemoveEyeTracker(eyeUpdater);

        public static void ChangeTrackerRingPosition(Vector2 position) =>
            _instance._eyeTrackerHandler.transform.position = Camera.main.WorldToScreenPoint(position);

        private void Awake() => _instance = this;

        private void OnDestroy() => _instance = null;
    }
}
