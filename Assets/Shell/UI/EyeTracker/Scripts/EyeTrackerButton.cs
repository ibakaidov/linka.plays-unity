using System;
using Shell;
using UnityEngine;

namespace Shell
{
    [RequireComponent(typeof(Collider2D))]
    public class EyeTrackerButton : MonoBehaviour, IEyeTrackerButton
    {
        public event Action Begun;
        public event Action Ended;

        private bool _state;
        
        private void FixedUpdate()
        {
            bool newState = RaycasterFromEyeButtons.Contains(this);
            if (_state != newState)
            {
                _state = newState;
                if (_state)
                    Begun?.Invoke();
                else
                    Ended?.Invoke();
            }
        }
    }
}