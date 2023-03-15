using System;

namespace Shell
{
    public interface IEyeTrackerButton
    {
        public event Action Begun;
        public event Action Ended;
    }
}
