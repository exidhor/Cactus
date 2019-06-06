using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class ColliderManager : MonoSingleton<ColliderManager>
{
    [Header("Chunks")]
    [SerializeField] Chunk _globalChunk;
    [SerializeField] List<Chunk> _chunks = new List<Chunk>();

    List<RectCollider> _buffer = new List<RectCollider>();

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] int _playerChunkPos = 0;

    private void Update()
    {
        float playerX = Player.instance.collider.rect.center.x;
        _playerChunkPos = Peaks(playerX);
    }
#endif

    public void GenerateChunks(Vector2 worldSize, float minY, int chunkCount)
    {
        float chunkSizeX = worldSize.x / chunkCount;
        Vector2 chunkSize = new Vector2(chunkSizeX, worldSize.y);

        float minX = -worldSize.x / 2;

        float currentX = minX;
        for (int i = 0; i < chunkCount; i++)
        {
            Chunk chunk = new Chunk(i, new Vector2(currentX, minY), chunkSize);

            _chunks.Add(chunk);

            currentX += chunkSize.x;
        }

        _globalChunk = new Chunk(-1, new Vector2(minX, minY), worldSize);
    }

    public void Register(RectCollider collider, string layer)
    {
        int chunkIndex = Peaks(collider.rect.center.x);
        Chunk chunk = GetChunk(chunkIndex);
        chunk.Add(collider, layer);
    }

    Chunk GetChunk(int index)
    {
        if (index < 0)
        {
            return _globalChunk;
        }
        else
        {
            return _chunks[index];
        }
    }

    int Peaks(float posX)
    {
        if (posX < _chunks[0].min.x)
            return -1;

        if (posX > _chunks[_chunks.Count - 1].max.x)
            return -1;

        Vector2i range = new Vector2i(0, _chunks.Count - 1);

        while (true)
        {
            range = BinarySearch(posX, range, _chunks);

            if (range.x == range.y) return range.x;
            else if (range.x > range.y) return -1;
        }
    }

    static Vector2i BinarySearch(float target, Vector2i range, List<Chunk> chunks)
    {
        int m = Mathf.RoundToInt(range.y - range.x) / 2 + range.x;

        float pd = chunks[m].min.x;
        float d = chunks[m].center.x;
        float nd = chunks[m].max.x;

        //if (m > 0)
        //{
        //    pd = chunks[m - 1].min.x;
        //}

        //if (m < range.y)
        //{
        //    nd = chunks[m + 1].max.x;
        //}

        if(pd <= target && target <= nd)
        {
            return new Vector2i(m, m);
        }

        //if (pd < target && target < d)
        //{
        //    float dp = target - pd;
        //    float dd = d - target;

        //    int index = dp < dd ? m - 1 : m;
        //    return new Vector2i(index, index);
        //}

        //if (d < target && target < nd)
        //{
        //    float dd = target - d;
        //    float dn = nd - target;

        //    int index = dd < dn ? m : m + 1;
        //    return new Vector2i(index, index);
        //}

        //if (d == target)
        //{
        //    return new Vector2i(m, m);
        //}

        //int o = range.y - range.x;

        if (d < target)
        {
            return new Vector2i(m + 1, range.y);
        }
        else
        {
            return new Vector2i(range.x, m - 1);
        }
    }

    public void Clear()
    {
        for(int i = 0; i < _chunks.Count; i++)
        {
            _chunks[i].Clear();
        }
    }

    public List<RectCollider> FindCollisions(Rect rect, string layer)
    {
        int chunkIndex = Peaks(rect.center.x);

        int minLeftIndex = chunkIndex;
        int maxRightIndex = chunkIndex;

        if (chunkIndex >= 0)
        {
            minLeftIndex = FindMinLeftChunk(chunkIndex, rect);
            maxRightIndex = FindMaxRightChunk(chunkIndex, rect);
        }
        
        _buffer.Clear();

        for (int i = minLeftIndex; i <= maxRightIndex; i++)
        {
            Chunk chunk = GetChunk(i);
            chunk.RetrieveNonAlloc(_buffer, rect, layer);
        }

        FilterCollisions(_buffer, rect);

        return _buffer;
    }

    int FindMinLeftChunk(int startIndex, Rect rect)
    {
        if (startIndex == 0) return 0;

        int minIndex = startIndex + 1;
        Chunk chunk;

        do
        {
            minIndex--;
            chunk = GetChunk(minIndex);
        }
        while (minIndex > 0 && chunk.min.x > rect.min.x);

        return minIndex;
    }

    int FindMaxRightChunk(int startIndex, Rect rect)
    {
        int last = _chunks.Count - 1;

        if (startIndex == last) return last;

        int maxIndex = startIndex - 1;
        Chunk chunk;

        do
        {
            maxIndex++;
            chunk = GetChunk(maxIndex);
        }
        while (maxIndex <= last && chunk.max.x < rect.max.x);

        return maxIndex;
    }

    void FilterCollisions(List<RectCollider> colliders, Rect rect)
    {
        int removeIndex = -1;
        int removeCount = 0;

        for (int i = 0; i < colliders.Count; i++)
        {
            RectCollider c = colliders[i];

            if (!c.rect.Overlaps(rect))
            {
                removeCount++;

                if (removeIndex == -1)
                {
                    removeIndex = i;
                }
            }
            else
            {
                if (removeIndex != -1)
                {
                    // swap with the first remove item
                    colliders[removeIndex] = c;
                    removeIndex++;
                }
            }
        }

        if (removeIndex > -1)
        {
            colliders.RemoveRange(removeIndex, removeCount);
        }
    }

    private void OnDrawGizmosSelected()
    {
        for(int i = 0; i < _chunks.Count; i++)
        {
            _chunks[i].OnDrawGizmos();
        }
    }
}