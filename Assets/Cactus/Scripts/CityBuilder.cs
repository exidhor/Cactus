using System;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    [Header("City Specs")]
    [SerializeField] Vector2 _worldSize;
    [SerializeField] float _minY;
    [SerializeField] int _chunkCount;

    [SerializeField] List<House> _houseLibrary;
    
    public void Generate()
    {
        ColliderManager.instance.GenerateChunks(_worldSize, _minY, _chunkCount);
    }
}
