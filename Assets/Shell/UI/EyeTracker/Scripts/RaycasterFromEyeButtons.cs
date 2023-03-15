using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shell
{
    public class RaycasterFromEyeButtons : MonoBehaviour
    {
        private const int _lengthBuffer = 32;
        
        private static RaycasterFromEyeButtons _instance;
        
        private readonly Collider2D[] _buffer = new Collider2D[_lengthBuffer];
        private HashSet<IEyeTrackerButton> _trackerButtons = new HashSet<IEyeTrackerButton>();
        private Camera _camera;
        private PointerEventData _pointerEventData;
        private List<RaycastResult> _pointerResults = new List<RaycastResult>();

        private bool _isUI;

        public static bool Contains(IEyeTrackerButton trackerButton) => _instance._isUI == false &&
            _instance._trackerButtons.Contains(trackerButton);

        private void Awake()
        {
            _camera = Camera.main;
            _pointerEventData = new PointerEventData(EventSystem.current);
        }

        private void OnEnable() => _instance = this;
        private void OnDisable() => _instance = null;

        private void FixedUpdate()
        {
            _pointerEventData.position = SightMaster.Point;
            EventSystem.current.RaycastAll(_pointerEventData, _pointerResults);
            _isUI = _pointerResults.Count > 0;
            
            _trackerButtons.Clear();
            
            Vector2 position = _camera.ScreenToWorldPoint(SightMaster.Point);
            int count = Physics2D.OverlapPointNonAlloc(position, _buffer);
            for (var i = 0; i < count; ++i)
            {
                if (_buffer[i].TryGetComponent(out IEyeTrackerButton trackerButton))
                {
                    _trackerButtons.Add(trackerButton);
                }
            }
        }
    }
}
        