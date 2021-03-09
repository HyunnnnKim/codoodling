using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octree<T>
{
    private OctreeNode<T> node = null;
    private int depth = 0;


    private int GetIndexOfPostion(Vector3 lookupPosition, Vector3 nodePosition)
    {

        return 0;
    }

    private class OctreeNode<T>
    {
        private Vector3 position = Vector3.zero;
        private OctreeNode<T>[] subNodes = null;
        private IList<T> value = null;
    }
}

public enum OctreeIndex
{
    UpperLeftFront,
    UpperRightFront,
    UpperLeftBack,
    UpperRightBack,
    BottomLeftFront,
    BottomRightFront,
    BottomLeftBack,
    BottomRightBack
}