using UnityEngine;

namespace Game9.Gameplay
{
    public abstract class BaseInteractiveObject : MonoBehaviour
    {
        public abstract void ShowHint();
        public abstract void Found();
    }
}
