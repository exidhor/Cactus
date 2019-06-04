using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class ColliderManager : MonoSingleton<ColliderManager>
{
    List<Chunk> _chunks = new List<Chunk>();

    Chunk _globalChunk;

    public void GenerateChunks(Vector2 worldSize, float minY, int chunkCount)
    {
        float chunkSizeX = worldSize.x / chunkCount;
        Vector2 chunkSize = new Vector2(chunkSizeX, worldSize.y);

        float minX = -worldSize.x / 2;

        float currentX = minX;
        for (int i = 0; i < chunkCount; i++)
        {
            Chunk district = new Chunk(i, new Vector2(currentX, minY), chunkSize);

            _chunks.Add(district);

            currentX += chunkSize.x;
        }

        _globalChunk = new Chunk(-1, new Vector2(minX, minY), worldSize);
    }

    public void Register(RectCollider collider, string layer)
    {
        int index = Peaks(collider.collider.center.x, _chunks);

        Chunk chunk;

        if(index < 0)
        {
            chunk = _globalChunk;
        }
        else
        {
            chunk = _chunks[index];
        }

        chunk.Add(collider, layer);
    }

    static int Peaks(float posX, List<Chunk> chunks)
    {
        if (posX < chunks[0].min.x)
            return -1;

        if (posX > chunks[chunks.Count - 1].max.x)
            return -1;

        Vector2i range = new Vector2i(0, chunks.Count - 1);

        while (true)
        {
            range = BinarySearch(posX, range, chunks);

            if (range.x == range.y) return range.x;
            else if (range.x > range.y) return -1;
        }
    }

    static Vector2i BinarySearch(float target, Vector2i range, List<Chunk> chunks)
    {
        int m = Mathf.RoundToInt(range.y - range.x) / 2 + range.x;

        float pd = float.MaxValue;
        float d = chunks[m].center.x;
        float nd = float.MinValue;

        if (m > 0)
        {
            pd = chunks[m - 1].center.x;
        }

        if (m < range.y)
        {
            nd = chunks[m + 1].center.x;
        }

        if (pd < target && target < d)
        {
            float dp = target - pd;
            float dd = d - target;

            int index = dp < dd ? m - 1 : m;
            return new Vector2i(index, index);
        }

        if (d < target && target < nd)
        {
            float dd = target - d;
            float dn = nd - target;

            int index = dd < dn ? m : m + 1;
            return new Vector2i(index, index);
        }

        if (d == target)
        {
            return new Vector2i(m, m);
        }

        int o = range.y - range.x;

        if (d < target)
        {
            return new Vector2i(m, range.y);
        }
        else
        {
            return new Vector2i(range.x, m);
        }
    }

    public void Actualize(float dt)
    {

    }

    private void OnDrawGizmosSelected()
    {
        for(int i = 0; i < _chunks.Count; i++)
        {
            _chunks[i].OnDrawGizmos();
        }
    }
}