using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    #region Serialized Field
    [Header("Grid Settings")]
    [SerializeField] private Vector2 cellSize = Vector2.zero;
    [SerializeField] private Vector2 spacing = Vector2.zero;
    [SerializeField] private FitType fitType = FitType.None;
    [SerializeField] private bool fitX = false;
    [SerializeField] private bool fitY = false;
    #endregion

    #region Private Field
    private int rows = 0;
    private int colums = 0;
    #endregion

    #region LayoutGroup Overrides
    public override void CalculateLayoutInputVertical()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            colums = Mathf.CeilToInt(sqrRt);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)colums);
        }
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            colums = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float)colums - ((spacing.x / (float)colums) * 2) - (padding.left / (float)colums) - (padding.right / (float)colums);
        float cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / colums;
            columnCount = i % colums;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void SetLayoutHorizontal()
    {
        
    }

    public override void SetLayoutVertical()
    {
        
    }
    #endregion

    public enum FitType { None, Uniform, Width, Height, FixedRows, FixedColumns }
}
