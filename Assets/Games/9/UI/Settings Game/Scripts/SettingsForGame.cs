using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game9.UI
{
    public class SettingsForGame : MonoBehaviour
    {
        [SerializeField] private GroupChoicer _groupChoicer;
        [SerializeField] private DifficultiesContainerView _difficultiesContainerView;

        public DifficultiesContainerView DifficultiesContainerView => _difficultiesContainerView;

        public GroupChoicer ChoicerLevelGeneratorType => _groupChoicer;
    }
}
