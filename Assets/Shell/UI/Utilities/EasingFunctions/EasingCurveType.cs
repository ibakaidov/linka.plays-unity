using UnityEngine;
using static Unity.Mathematics.math;

public enum EasingCurveType
{
    InSine,
    OutSine,
    InOutSine,

    InCubic,
    OutCubic,
    InOutCubic,

    InQuint,
    OutQuint,
    InOutQuint,

    InCirc,
    OutCirc,
    InOutCirc,

    InElastic,
    OutElastic,
    InOutElastic,

    InQuad,
    OutQuad,
    InOutQuad,

    InQuart,
    OutQuart,
    InOutQuart,

    InExpo,
    OutExpo,
    InOutExpo,

    InBack,
    OutBack,
    InOutBack,

    InBounce,
    OutBounce,
    InOutBounce,
};

[System.Serializable]
public struct EasingCurve
{
    [SerializeField] private EasingCurveType _curveType;

    public float Evaluate(float time) => CurveType.Evaluate(time, _curveType);
}

public static class CurveType
{
    public static float Evaluate(float x, EasingCurveType curveType)
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;
        const float c3 = c1 + 1f;
        const float c4 = (2 * PI) / 3;
        const float c5 = (2 * PI) / 4.5f;
        
        switch (curveType)
        {
            case EasingCurveType.InSine:
                return 1 - cos(x * PI) / 2;
            case EasingCurveType.OutSine:
                return sin((x * PI) / 2);
            case EasingCurveType.InOutSine:
                return -(cos(PI * x) - 1) / 2;

            case EasingCurveType.InCubic:
                return x * x * x;
            case EasingCurveType.OutCubic:
                return 1 - pow(1 - x, 3);
            case EasingCurveType.InOutCubic:
                return x < 0.5 ? 4 * x * x * x : 1 - pow(-2 * x + 2, 3) / 2;
            
            case EasingCurveType.InQuint:
                return x * x * x * x * x;
            case EasingCurveType.OutQuint:
                return 1 - pow(1 - x, 5);
            case EasingCurveType.InOutQuint:
                return x < 0.5 ? 16 * x * x * x * x * x : 1 - pow(-2 * x + 2, 5) / 2;
            
            case EasingCurveType.InCirc:
                return 1 - sqrt(1 - pow(x, 2));;
            case EasingCurveType.OutCirc:
                return sqrt(1 - pow(x - 1, 2));
            case EasingCurveType.InOutCirc:
                return x < 0.5
                    ? (1 - sqrt(1 - pow(2 * x, 2))) / 2
                    : (sqrt(1 - pow(-2 * x + 2, 2)) + 1) / 2;
            
            case EasingCurveType.InElastic:
                return x == 0
                    ? 0
                    : x == 1
                        ? 1
                        : -pow(2, 10 * x - 10) * sin((x * 10 - 10.75f) * c4);
            case EasingCurveType.OutElastic:
                return x == 0
                    ? 0
                    : x == 1
                        ? 1
                        : pow(2, -10 * x) * sin((x * 10 - 0.75f) * c4) + 1;
            case EasingCurveType.InOutElastic:
                return x == 0
                    ? 0
                    : x == 1
                        ? 1
                        : x < 0.5
                            ? -(pow(2, 20 * x - 10) * sin((20 * x - 11.125f) * c5)) / 2
                            : (pow(2, -20 * x + 10) * sin((20 * x - 11.125f) * c5)) / 2 + 1;
            
            case EasingCurveType.InQuad:
                return x * x;
            case EasingCurveType.OutQuad:
                return 1 - (1 - x) * (1 - x);
            case EasingCurveType.InOutQuad:
                return x < 0.5 ? 2 * x * x : 1 - pow(-2 * x + 2, 2) / 2;
            
            case EasingCurveType.InQuart:
                return x * x * x * x;
            case EasingCurveType.OutQuart:
                return 1 - pow(1 - x, 4);
            case EasingCurveType.InOutQuart:
                return x < 0.5 ? 8 * x * x * x * x : 1 - pow(-2 * x + 2, 4) / 2;
            
            case EasingCurveType.InExpo:
                return x == 0 ? 0 : pow(2, 10 * x - 10);
            case EasingCurveType.OutExpo:
                return x == 1 ? 1 : 1 - pow(2, -10 * x);
            case EasingCurveType.InOutExpo:
                return x == 0
                    ? 0
                    : x == 1
                        ? 1
                        : x < 0.5 ? pow(2, 20 * x - 10) / 2
                            : (2 - pow(2, -20 * x + 10)) / 2;

            case EasingCurveType.InBack:
                return c3 * x * x * x - c1 * x * x;
            case EasingCurveType.OutBack:
                return 1 + c3 * pow(x - 1, 3) + c1 * pow(x - 1, 2);
            case EasingCurveType.InOutBack:
                return x < 0.5
                    ? (pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
                    : (pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
            
            case EasingCurveType.InBounce:
                return 1 - Evaluate(1 - x, EasingCurveType.OutBounce);
            case EasingCurveType.OutBounce:
                const float n1 = 7.5625f;
                const float d1 = 2.75f;

                if (x < 1 / d1) {
                    return n1 * x * x;
                } else if (x < 2 / d1) {
                    return n1 * (x -= 1.5f / d1) * x + 0.75f;
                } else if (x < 2.5 / d1) {
                    return n1 * (x -= 2.25f / d1) * x + 0.9375f;
                } else {
                    return n1 * (x -= 2.625f / d1) * x + 0.984375f;
                }
            case EasingCurveType.InOutBounce:
                return x < 0.5
                    ? (1 - Evaluate(1 - 2 * x, EasingCurveType.OutBounce)) / 2
                    : (1 + Evaluate(2 * x - 1, EasingCurveType.OutBounce)) / 2;
            
            default:
                throw new System.ArgumentOutOfRangeException(nameof(curveType), curveType, null);
        }
    }
}