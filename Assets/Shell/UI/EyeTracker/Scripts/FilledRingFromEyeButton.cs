using UnityEngine;

namespace Shell
{
    public class FilledRingFromEyeButton : MonoBehaviour
    {
        [SerializeField] private CircleFill _circleFill;

        private void Awake() => EyeTrackerHandler.ChangeRatio += UpdateRatio;
        private void OnDestroy() => EyeTrackerHandler.ChangeRatio -= UpdateRatio;

        public void UpdateRatio(float ratio) => _circleFill.Amount = ratio;
    }
}
