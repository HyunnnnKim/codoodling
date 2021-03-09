using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(CurvePointsCreator))]
public class CurvesEditor : Editor
{
    #region Private Field
    private CurvePointsCreator _creator = null;
    private CurvePoints _curve = null;
    private CurveType _selectedCurve = CurveType.Linear;
    private bool _useTest = false;
    #endregion

    private void OnEnable()
    {
        InitVariables();
    }

    #region Initialize
    private void InitVariables()
    {
        _creator = target as CurvePointsCreator;
        if (_creator.CurvePoints == null)
        {
            if (_useTest) _creator.CreateTestPoints();
            else _creator.CreateCurvePoints();
        }
        _curve = _creator.CurvePoints;
    }
    #endregion

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("CREATE MODE");
        GUILayout.Space(2);
        if (GUILayout.Button("Switch Mode"))
        {
            _useTest = !_useTest;
            if (_useTest) _creator.CreateTestPoints();
            else _creator.CreateCurvePoints();
            _curve = _creator.CurvePoints;
        }
        GUILayout.Space(15);

        if (_useTest)
        {
            _selectedCurve = (CurveType)EditorGUILayout.EnumPopup("Selected Curve : ", _selectedCurve);
            GUILayout.Space(5f);

            if (GUILayout.Button("Create Curve"))
            {
                _creator.SelectedCurve = _selectedCurve;
                _creator.CreateTestPoints();
                _curve = _creator.CurvePoints;
            }
        }
        else
        {

        }
    }

    [System.Obsolete]
    private void OnSceneGUI()
    {
        /* testing has obsolete */
        if (_useTest)
        {
            DrawTestPoints();
            DrawTestLines();
        }
        else
        {
            Input();
            DrawCurvePoints();
            DrawCurveLines();
        }
    }

    #region Input Controls
    private void Input()
    {
        Event guiEvent = Event.current;
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Undo.RecordObject(_creator, "Add segment");
            _curve.AddSegment(mousePos);
        }
    }
    #endregion

    #region Draw Test Curves
    private void DrawTestPoints()
    {
        Handles.color = Color.red;

        for (int i = 0; i < _curve.Points.Count; i++)
        {
            Vector3 newPos = Handles.FreeMoveHandle(_curve.Points[i], Quaternion.identity, 0.05f,
                Vector3.zero, Handles.CylinderHandleCap);
            if (_curve.Points[i] != newPos)
            {
                Undo.RecordObject(_creator, "Move Point");
                _curve.Points[i] = newPos;
            }
        }
    }

    [System.Obsolete]
    private void DrawTestLines()
    {
        Vector3[] points = _curve.GetPoints();
        Handles.color = Color.green * 0.8f;

        switch (_creator.SelectedCurve)
        {
            case CurveType.Linear:
                for (float t = 0; t <= 1; t += 0.01f)
                {
                    var point = Curves.LinearBezierCurves(points[0], points[1], t);
                    Handles.SphereCap(0, point, Quaternion.identity, 0.02f); // Obsolete
                }
                break;
            case CurveType.Quadratic:
                for (float t = 0; t <= 1; t += 0.01f)
                {
                    var point = Curves.QuadraticBezierCurves(points[0], points[1], points[2], t);
                    Handles.SphereCap(0, point, Quaternion.identity, 0.02f); // Obsolete
                }
                break;
            case CurveType.Cubic:
                for (float t = 0; t <= 1; t += 0.01f)
                {
                    var point = Curves.CubicBezierCurves(points[0], points[1], points[2], points[3], t);
                    Handles.SphereCap(0, point, Quaternion.identity, 0.02f); // Obsolete
                }
                break;
        }
    }
    #endregion

    #region Draw Path
    /// <summary>
    /// Draw every points(anchor points, control points) with handles.
    /// </summary>
    private void DrawCurvePoints()
    {
        Handles.color = Color.red;

        for (int i = 0; i < _curve.Points.Count; i++)
        {
            Vector3 newPos = Handles.FreeMoveHandle(_curve.Points[i], Quaternion.identity, 0.08f,
                Vector3.zero, Handles.CylinderHandleCap);
            if (_curve.Points[i] != newPos)
            {
                Undo.RecordObject(_creator, "Move Point");
                _curve.MovePoint(i, newPos);
            }
        }
    }

    /// <summary>
    /// Draw Bezier curve with control lines.
    /// </summary>
    private void DrawCurveLines()
    {
        for (int i = 0; i < _curve.SegmentCount; i++)
        {
            Vector3[] points = _curve.GetPointsInSegment(i);
            Handles.color = Color.black;
            Handles.DrawLine(points[1], points[0]);
            Handles.DrawLine(points[2], points[3]);
            Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
        }
    }
    #endregion
}