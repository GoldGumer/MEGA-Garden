using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTransform : MonoBehaviour
{
    MyVector3[] meshVertices;
    MyVector3[] transformedVertices;

    [SerializeField] MyVector3 scale;
    [SerializeField] MyVector3 rotation;
    [SerializeField] MyVector3 position;

    MyVector3 eularRotation;
    // Private Functions
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        meshVertices = new MyVector3[mesh.vertexCount];
        int i = 0;
        foreach (Vector3 vertex in mesh.vertices)
        {
            meshVertices[i] = new MyVector3(vertex);
            i++;
        }
    }

    void UpdateMesh()
    {
        eularRotation = new MyVector3(
            rotation.x / (180 / Mathf.PI),
            rotation.y / (180 / Mathf.PI),
            rotation.z / (180 / Mathf.PI)
            );

        transformedVertices = new MyVector3[meshVertices.Length];

        MyMatrix4x4 
            mScale, 
            mRotation,
            mTranslation,
            mTranformation;

        //Scaling

        mScale = new MyMatrix4x4(
            new MyVector4(1 * scale.x, 0, 0, 0),
            new MyVector4(0, 1 * scale.y, 0, 0),
            new MyVector4(0, 0, 1 * scale.z, 0),
            new MyVector4(0, 0, 0, 1)
            );

        //Rotation

        MyMatrix4x4
            roll = new MyMatrix4x4(
            new MyVector4(Mathf.Cos(eularRotation.x), -Mathf.Sin(eularRotation.x), 0, 0),
            new MyVector4(Mathf.Sin(eularRotation.x), Mathf.Cos(eularRotation.x), 0, 0),
            new MyVector4(0, 0, 1, 0),
            new MyVector4(0, 0, 0, 1)
            ),
            pitch = new MyMatrix4x4(
            new MyVector4(1, 0, 0, 0),
            new MyVector4(0, Mathf.Cos(eularRotation.y), -Mathf.Sin(eularRotation.y), 0),
            new MyVector4(0, Mathf.Sin(eularRotation.y), Mathf.Cos(eularRotation.y), 0),
            new MyVector4(0, 0, 0, 1)
            ),
            yaw = new MyMatrix4x4(
            new MyVector4(Mathf.Cos(eularRotation.z), 0, Mathf.Sin(eularRotation.z), 0),
            new MyVector4(0, 1, 0, 0),
            new MyVector4(-Mathf.Sin(eularRotation.z), 0, Mathf.Cos(eularRotation.z), 0),
            new MyVector4(0, 0, 0, 1)
            );

        mRotation = yaw * (pitch * roll);

        //Translation

        mTranslation = new MyMatrix4x4(
            new MyVector4(1, 0, 0, position.x),
            new MyVector4(0, 1, 0, position.y),
            new MyVector4(0, 0, 1, position.z),
            new MyVector4(0, 0, 0, 1)
            );

        //Apply

        mTranformation = (mTranslation * (mRotation * mScale));
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        int i = 0;
        foreach (MyVector3 vertex in meshVertices)
        {
            transformedVertices[i] = mTranformation * vertex;
            i++;
        }

        //Convert To Unity Vector3

        Vector3[] unityVertices = new Vector3[transformedVertices.Length];
        i = 0;
        foreach (MyVector3 vertex in transformedVertices)
        {
            unityVertices[i] = vertex.getUnityVector3();
            i++;
        }

        mesh.vertices = unityVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    private void Update()
    {
        UpdateMesh();
    }

    //Public Functions
}
