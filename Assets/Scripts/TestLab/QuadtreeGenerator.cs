using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class QuadtreeGenerator : MonoBehaviour
{
    #region Serialize Field
    [SerializeField] private float _size = 5f;
    [SerializeField] private int _depth = 2;
    #endregion

    #region Private Field
    private Color minColor = new Color(1f, 1f, 1f, 1f);
    private Color maxColor = new Color(0f, 0.5f, 1f, 0.25f);
    #endregion

    private void OnDrawGizmos()
    {
        var quadtree = new Quadtree<int>(this.transform.position, _size, _depth);
        DrawNode(quadtree.GetRoot());
    }

    private void DrawNode(Quadtree<int>.QuadtreeNode<int> node, int nodeDepth = 0)
    {
        if (!node.IsLeaf())
        {
            foreach (var subnode in node.Nodes)
            {
                DrawNode(subnode, nodeDepth + 1);
            }
        }
        Gizmos.color = Color.Lerp(minColor, maxColor, nodeDepth / (float)_depth);
        Gizmos.DrawWireCube(node.Position, Vector2.one * node.Size);
    }
}

public class Quadtree<T>
{
    #region Private Field
    private QuadtreeNode<T> _node = null;
    private int _depth = 0;
    #endregion

    public Quadtree(Vector2 position, float size, int depth)
    {
        _node = new QuadtreeNode<T>(position, size);
        _node.Subdivide(depth);
    }

    private int GetIndexOfPosition(Vector2 lookupPosition, Vector2 nodePosition)
    {
        int index = 0;

        index |= lookupPosition.y > nodePosition.y ? 2 : 0;
        index |= lookupPosition.x > nodePosition.x ? 1 : 0;

        return index;
    }

    public QuadtreeNode<T> GetRoot()
    {
        return _node;
    }

    public class QuadtreeNode<T>
    {
        #region Private Field
        private Vector2 _position = Vector2.zero;
        private float _size = 0f;
        private QuadtreeNode<T>[] _subNodes = null;
        private IList<T> _value = null;
        #endregion

        public QuadtreeNode(Vector2 position, float size)
        {
            _position = position;
            _size = size;
        }

        #region Properties
        public Vector2 Position { get => _position; }
        public float Size { get => _size; }
        public IEnumerable<QuadtreeNode<T>> Nodes { get => _subNodes; }
        #endregion

        public void Subdivide(int depth = 0)
        {
            _subNodes = new QuadtreeNode<T>[4];
            for (int i = 0; i < _subNodes.Length; ++i)
            {
                Vector2 newPosition = _position;
                if ((i & 2) == 2)
                {
                    newPosition.y += _size * 0.25f;
                }
                else
                {
                    newPosition.y -= _size * 0.25f;
                }

                if ((i & 1) == 1)
                {
                    newPosition.x += _size * 0.25f;
                }
                else
                {
                    newPosition.x -= _size * 0.25f;
                }

                _subNodes[i] = new QuadtreeNode<T>(newPosition, _size * 0.5f);
                if (depth > 0)
                {
                    _subNodes[i].Subdivide(depth - 1);
                }
            }
        }

        public bool IsLeaf()
        {
            return _subNodes == null;
        }
    }
}

public enum QuadtreeIndex
{
    /* 0: 00, 1: 01, 2: 10, 3:11*/
    TopLeft = 0,
    TopRight = 1,
    BottomLeft = 2,
    BottomRight = 3
}