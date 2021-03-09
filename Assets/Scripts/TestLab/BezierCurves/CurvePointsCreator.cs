using UnityEngine;

public class CurvePointsCreator : MonoBehaviour
{
    [HideInInspector]
    public CurvePoints CurvePoints { get; private set; }
    public CurveType SelectedCurve { get; set; } = CurveType.Linear;

    /* Used for testing. */
    public void CreateTestPoints()
    {
        CurvePoints = new CurvePoints(transform.position, SelectedCurve);
    }

    /// <summary>
    /// Construct path points.
    /// </summary>
    public void CreateCurvePoints()
    {
        CurvePoints = new CurvePoints(transform.position);
    }
}