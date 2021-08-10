using UnityEngine;

[CreateAssetMenu(fileName = "AnimationCurvesPreset", menuName = "Codoodling/AnimationCurves")]
public class AnimationCurvesPreset : ScriptableObject
{
    #region Serialized Field
    [Header("Curves")]
    [SerializeField] private AnimationCurve easeOut = null;

    #endregion

    #region Properties
    public AnimationCurve EaseOut => easeOut;
    #endregion
}
