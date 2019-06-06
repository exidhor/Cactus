using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class CityBuilder : MonoBehaviour
{
    [Header("World")]
    [SerializeField] bool _generateRandomSeed;
    [SerializeField] int _seed;
    [SerializeField] Vector2 _worldSize;

    [Header("Chunk Specs")]
    [SerializeField] float _minY;
    [SerializeField] int _chunkCount;

    [Header("Decor Specs")]
    [SerializeField] Vector2i _groupRange;
    [SerializeField] Vector2 _offsetRangeInGroup;
    [SerializeField] Vector2 _offsetRangeBetweenGroups;
    [SerializeField] float _yBase;
    [SerializeField] float _decorDepth;

    [Header("Resources")]
    [SerializeField] List<Decor> _decorLibrary;

    RandomGenerator _random;
    
    public void Generate()
    {
        InitRandom();
        ColliderManager.instance.GenerateChunks(_worldSize, _minY, _chunkCount);

        GenerateDecors();
    }

    void InitRandom()
    {
        int seed = _seed;

        if(_generateRandomSeed)
        {
            DateTime currentTime = DateTime.Now;
            seed = currentTime.Ticks.ToString().GetHashCode();
        }

        _random = new RandomGenerator(seed);
    }

    void GenerateDecors()
    {
        float xMax = _worldSize.x / 2;
        float xMin = -xMax;

        float x = xMin;
        int currentGroupSize = 0;
        int targetGroupSize = 0;

        while (x < xMax)
        {
            if(currentGroupSize >= targetGroupSize)
            {
                float offsetBetweenGroups = _random.NextFloat(_offsetRangeBetweenGroups);
                x += offsetBetweenGroups;

                currentGroupSize = 0;
                targetGroupSize = _random.Next(_groupRange);
            }

            int decorIndex = _random.Next(_decorLibrary.Count);
            Decor decor = Instantiate(_decorLibrary[decorIndex], x);

            x += decor.collider.rect.size.x;
            currentGroupSize++;

            if(currentGroupSize < targetGroupSize)
            {
                x += _random.NextFloat(_offsetRangeInGroup);
            }
        }
    }

    Decor Instantiate(Decor decor, float x)
    {
        Decor d = GameObject.Instantiate(decor);
        d.transform.parent = transform;
        d.transform.position = new Vector3(x, _yBase, _decorDepth);
        d.Init(_random);

        return d;
    }
}
