using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mesh_Generator : MonoBehaviour
{
    [SerializeField] float pixelMultiplier;
    [SerializeField] int gridSize;
    [SerializeField] Vector2Int fieldSize;

    public bool randomise = false;

    Perlin_Noise_Manager noiseManager;

    Texture2D noiseTexture;

    //2D mesh for the play area
    Mesh field;

    //vertex and triangle lists
    Vector3[] vertices;
    int[] trianglesList;

    // Start is called before the first frame update
    void Start()
    {
        noiseTexture = new Texture2D(fieldSize.x, fieldSize.y, TextureFormat.RGB24, false);

        noiseManager = GetComponent<Perlin_Noise_Manager>();
        noiseManager.SetParameters(gridSize, fieldSize);
        noiseManager.GeneratePerlinNoise();
        RenderTexture.active = noiseManager.GetPerlinTexture();

        noiseTexture.ReadPixels(new Rect(new Vector2(0, 0), fieldSize), 0, 0);

        field = new Mesh();
        GetComponent<MeshFilter>().mesh = field;
        GenerateFaces();
        UpdateMesh();
    }

    void GenerateFaces()
    {
        int x = fieldSize.x - 2;
        int y = fieldSize.y - 2;
        vertices = new Vector3[(x + 1) * (y + 1)];
        for (int i = 0; i < (y + 1); i++)
        {
            for (int j = 0; j < (x + 1); j++)
            {
                vertices[j + i * (x + 1)] = new Vector3(i - Mathf.Floor(x / 2), noiseTexture.GetPixel(j,i).r * pixelMultiplier, j - Mathf.Floor(y / 2));
            }
        }

        trianglesList = new int[x * y * 2 * 3];
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                int offset = x + 1;
                int indexOffset = j + (i * offset);
                int arrayOffset = j * 6 + i * 6 * x;

                trianglesList[arrayOffset] = indexOffset + offset;
                trianglesList[arrayOffset + 1] = indexOffset;
                trianglesList[arrayOffset + 2] = indexOffset + 1;

                trianglesList[arrayOffset + 3] = indexOffset + offset + 1;
                trianglesList[arrayOffset + 4] = indexOffset + offset;
                trianglesList[arrayOffset + 5] = indexOffset + 1;
            }
        }
    }

    void UpdateMesh()
    {
        field.Clear();
        field.vertices = vertices;
        field.triangles = trianglesList;
        field.RecalculateNormals();
        field.RecalculateTangents();
        GetComponent<MeshCollider>().sharedMesh = field;
    }

    public void Randomise()
    {
        noiseManager.Randomize();
        noiseManager.SetParameters(gridSize, fieldSize);
        noiseManager.GeneratePerlinNoise();
        RenderTexture.active = noiseManager.GetPerlinTexture();
        noiseTexture.ReadPixels(new Rect(new Vector2(0, 0), fieldSize), 0, 0);
        GenerateFaces();
        UpdateMesh();
    }

    private void Update()
    {
        if (randomise)
        {
            Randomise();
            randomise = false;
        }
    }
}
