using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shell
{
    public interface IEyeUpdater
    {
        void UpdateEyeRation(float ratio);
        void ClickFromEye();
        bool IsValid { get; }
    }
}
