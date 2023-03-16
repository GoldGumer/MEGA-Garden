using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texture_Alloctor : MonoBehaviour
{
    [SerializeField] const int maxFieldSize = 256;
    [SerializeField] Vector2Int totalFieldSize;
    [SerializeField] int gridSize;

    Perlin_Noise_Manager noiseManager;

    int numberOfFields;

    private void Start()
    {
        numberOfFields = Mathf.CeilToInt(totalFieldSize.x * totalFieldSize.y / (maxFieldSize * maxFieldSize));
        noiseManager = GetComponent<Perlin_Noise_Manager>();
        noiseManager.SetParameters(gridSize, totalFieldSize);


    }
}
