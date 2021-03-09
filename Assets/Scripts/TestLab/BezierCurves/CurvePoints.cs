using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurvePoints
{
    [SerializeField, HideInInspector]
    public List<Vector3> Points { get; private set; }

    /* Used for testing */
    public CurvePoints(Vector3 center, CurveType curves)
    {
        switch (curves)
        {
            case CurveType.Linear:
                Points = new List<Vector3>
                {
                    center + Vector3.left,
                    center + Vector3.right
                };
                break;
            case CurveType.Quadratic:
                Points = new List<Vector3>
                {
                    center + Vector3.left * 2f,
                    center + Vector3.up,
                    center + Vector3.right * 2f
                };
                break;
            case CurveType.Cubic:
                Points = new List<Vector3>
                {
                    center + Vector3.left * 2f,
                    center + Vector3.left * 0.8f + Vector3.up,
                    center + Vector3.right * 0.8f + Vector3.down,
                    center + Vector3.right * 2f
                };
                break;
        }
    }

    /// <summary>
    /// Used for path creation.
    /// </summary>
    /// <param name="center"></param>
    public CurvePoints(Vector3 center)
    {
        Points = new List<Vector3>
        {
            center + Vector3.left,
            center + (Vector3.left + Vector3.up) * 0.5f,
            center + (Vector3.right + Vector3.down) * 0.5f,
            center + Vector3.right
        };
    }

    #region Controls
    /* Used for testing */
    public Vector3[] GetPoints()
    {
        return Points.ToArray();
    }

    /// <summary>
    /// 입력받은 인덱스에 해당하는 세그먼트를 반환한다.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public Vector3[] GetPointsInSegment(int i)
    {
        /* Returns only 3 points because the last point is shared with next segment. */
        return new Vector3[] { Points[i * 3], Points[i * 3 + 1], Points[i * 3 + 2], Points[i * 3 + 3] };
    }

    /// <summary>
    /// Returns the number of segments.
    /// </summary>
    public int SegmentCount => (Points.Count - 4) / 3 + 1;

    /// <summary>
    /// 새로운 Anchor Point를 추가한다.
    /// </summary>
    /// <param name="anchorPos"></param>
    public void AddSegment(Vector3 anchorPos)
    {
        /* Adding one control point to the old anchor point. */
        Points.Add(Points[Points.Count - 1] * 2 - Points[Points.Count - 2]);
        /* Adding one control point to the New anchor point. */
        Points.Add((Points[Points.Count - 1] + anchorPos) * 0.5f);
        /* Adding new anchor point. */
        Points.Add(anchorPos);
    }

    public void MovePoint(int i, Vector3 pos)
    {
        Vector3 deltaMove = pos - Points[i];
        Points[i] = pos;

        if (i % 3 == 0)
        {
            if (i + 1 < Points.Count)
            {
                Points[i + 1] += deltaMove;
            }
            if (i - 1 >= 0)
            {
                Points[i - 1] += deltaMove;
            }
        }
        else
        {
            bool nextPointIsAnchor = (i + 1) % 3 == 0;
            int correspondingControlIndex = nextPointIsAnchor ? i + 2 : i - 2;
            int anchorIndex = nextPointIsAnchor ? i + 1 : i - 1;

            if (correspondingControlIndex >= 0 && correspondingControlIndex < Points.Count)
            {
                float dst = (Points[anchorIndex] - Points[correspondingControlIndex]).magnitude;
                Vector3 dir = (Points[anchorIndex] - pos).normalized;
                Points[correspondingControlIndex] = Points[anchorIndex] + dir * dst;
            }
        }
    }
    #endregion
}

public enum CurveType { Linear, Quadratic, Cubic };