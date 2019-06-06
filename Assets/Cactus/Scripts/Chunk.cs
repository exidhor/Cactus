using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

[Serializable]
public class Chunk
{
    [Serializable]
    public class QuadTrees
    {
        public QuadTree<RectCollider> dynamicQT;
        public QuadTree<RectCollider> staticQT;

        public QuadTrees(Rect bounds)
        {
            dynamicQT = new QuadTree<RectCollider>(bounds);
            staticQT = new QuadTree<RectCollider>(bounds);
        }

        public void Insert(RectCollider collider)
        {
            QuadTree<RectCollider> quadTree = collider.isDynamic ? dynamicQT : staticQT;

            quadTree.Insert(collider, collider.rect);
        }

        public List<RectCollider> Retrieve(Rect rect)
        {
            List<RectCollider> found = dynamicQT.Retrieve(rect);
            found.AddRange(staticQT.Retrieve(rect));

            return found;
        }

        public void RetrieveNonAlloc(List<RectCollider> toFill, Rect rect)
        {
            dynamicQT.RetrieveNonAlloc(toFill, rect);
            staticQT.RetrieveNonAlloc(toFill, rect);
        }
    }

    public Vector2 center
    {
        get { return _center; }
    }

    public Vector2 min
    {
        get { return _pos; }
    }

    public Vector2 max
    {
        get { return _pos + _size; }
    }

    int _index;
    Vector2 _pos;
    Vector2 _size;

    Vector2 _center;
    Rect _bounds;

    Dictionary<string, QuadTrees> _quadTreeDict = new Dictionary<string, QuadTrees>();
    [SerializeField] List<QuadTrees> _quadTreeList = new List<QuadTrees>();

    public Chunk(int index, Vector2 pos, Vector2 size)
    {
        _index = index;
        _pos = pos;
        _size = size;

        _center = _pos + _size / 2;
        _bounds = new Rect(_pos, _size);
    }

    public void Add(RectCollider collider, string layer)
    {
        if (!_quadTreeDict.ContainsKey(layer))
        {
            QuadTrees qt = new QuadTrees(_bounds);

            _quadTreeDict.Add(layer, qt);
            _quadTreeList.Add(qt);
        }

        _quadTreeDict[layer].Insert(collider);
    }

    public List<RectCollider> Retrieve(Rect rect, string layer)
    {
        if (!_quadTreeDict.ContainsKey(layer)) return new List<RectCollider>();

        QuadTrees qts = _quadTreeDict[layer];
        List<RectCollider> found = qts.Retrieve(rect);

        return found;
    }

    public void RetrieveNonAlloc(List<RectCollider> toFill, Rect rect, string layer)
    {
        if (!_quadTreeDict.ContainsKey(layer)) return;

        QuadTrees qts = _quadTreeDict[layer];
        qts.RetrieveNonAlloc(toFill, rect);
    }

    public void Clear()
    {
        for(int i = 0; i < _quadTreeList.Count; i++)
        {
            _quadTreeList[i].dynamicQT.Clear();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
    }

    public override string ToString()
    {
        return base.ToString() + " count=" + _quadTreeList.Count;
    }
}
