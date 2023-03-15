using System.Collections.Generic;
using Game9.Gameplay;

namespace Game9.Levels
{
    public interface ILevelGenerator
    {
        void Init(LevelGenerator levelGenerator);
        List<Obstacle> Generate();
        void PrepareForNextGeneration();
    }
}
