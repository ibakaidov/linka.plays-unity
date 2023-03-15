using System;
using Game6.UI;
using Shell;
using UnityEngine;

namespace Game6
{
    [System.Serializable]
    public class LineUIData
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _label;
        [SerializeField] private Vector2 _minMaxScale = new Vector2(1f, 10f);

        public Sprite Icon => _icon;
        public string Label => _label;

        public Vector2 MinMaxScale => _minMaxScale;
    }
    
    [System.Serializable]
    public class LineInfo
    {
        [SerializeField] private LineUIData _uiData;
        [SerializeField] private SpawnerInteractiveObjects _spawnerInteractiveObjects;
        [SerializeField] private LineData _lineData;

        public LineUIData UIData => _uiData;
        public LineData Data => _lineData;
        public SpawnerInteractiveObjects Spawner => _spawnerInteractiveObjects;
    }

    public class LinesContainer : MonoBehaviour
    {
        [SerializeField] private LineInfo[] _lineInfos;
        [SerializeField] private LineConfigurationView _lineConfigurationViewPrefab;
        [SerializeField] private AudioClip _clipFromDestroyedObject;

        private bool _isInited;

        private void OnEnable()
        {
            if (_isInited == false)
            {
                Init();
                _isInited = true;
            }
            
            foreach (var lineInfo in _lineInfos)
                LineTurnOn(lineInfo);
        }

        private void OnDisable()
        {
            foreach (var lineInfo in _lineInfos)
                LineTurnOff(lineInfo);
        }

        private void Init()
        {
            foreach (var lineInfo in _lineInfos)
            {
                var view = Instantiate(_lineConfigurationViewPrefab, UIMediator.ContainerForConfigurations);
                view.Enabled += (value) =>
                {
                    if (value)
                        LineTurnOn(lineInfo);
                    else
                        LineTurnOff(lineInfo);
                };
                view.ChangedScale += (value) =>
                {
                    lineInfo.Spawner.ChangeScale(value);
                };
                
                view.Init(lineInfo.UIData.Label, lineInfo.Data.DefaultScale, lineInfo.UIData.MinMaxScale);
                lineInfo.Spawner.SetData(lineInfo.Data);

                lineInfo.Spawner.DestroyedObject += () => UIMediator.Scoreboard.IncrementScore(lineInfo);
            }
        }
        
        private void LineTurnOn(LineInfo lineInfo)
        {
            lineInfo.Spawner.TurnOn();
            lineInfo.Spawner.DestroyedObject += OnDestroyedObject;
            UIMediator.Scoreboard.Add(lineInfo);
        }

        private void LineTurnOff(LineInfo lineInfo)
        {
            lineInfo.Spawner.TurnOff();
            lineInfo.Spawner.DestroyedObject -= OnDestroyedObject;
            UIMediator.Scoreboard.Remove(lineInfo);
        }

        private void OnDestroyedObject()
        {
            SoundsHandler.PlayOneShot(_clipFromDestroyedObject);
        }
    }
}
