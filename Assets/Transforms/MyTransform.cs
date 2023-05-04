using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTransform : MonoBehaviour
{
    MyVector3[] meshVertices;
    MyVector3[] transformedVertices;

    [SerializeField] MyVector3 scale;
    [SerializeField] List<MyVector4> rotations;
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
        transformedVertices = new MyVector3[meshVertices.Length];

        MyMatrix4x4
            mScale,
            mTranslation;

        //Scaling

        mScale = new MyMatrix4x4(
            new MyVector4(1 * scale.x, 0, 0, 0),
            new MyVector4(0, 1 * scale.y, 0, 0),
            new MyVector4(0, 0, 1 * scale.z, 0),
            new MyVector4(0, 0, 0, 1)
            );

        /*//Rotation

        MyMatrix4x4
            roll = new MyMatrix4x4(
            new MyVector4(Mathf.Cos(eularRotation.z), -Mathf.Sin(eularRotation.z), 0, 0),
            new MyVector4(Mathf.Sin(eularRotation.z), Mathf.Cos(eularRotation.z), 0, 0),
            new MyVector4(0, 0, 1, 0),
            new MyVector4(0, 0, 0, 1)
            ),
            pitch = new MyMatrix4x4(
            new MyVector4(1, 0, 0, 0),
            new MyVector4(0, Mathf.Cos(eularRotation.x), -Mathf.Sin(eularRotation.x), 0),
            new MyVector4(0, Mathf.Sin(eularRotation.x), Mathf.Cos(eularRotation.x), 0),
            new MyVector4(0, 0, 0, 1)
            ),
            yaw = new MyMatrix4x4(
            new MyVector4(Mathf.Cos(eularRotation.y), 0, Mathf.Sin(eularRotation.y), 0),
            new MyVector4(0, 1, 0, 0),
            new MyVector4(-Mathf.Sin(eularRotation.y), 0, Mathf.Cos(eularRotation.y), 0),
            new MyVector4(0, 0, 0, 1)
            );

        mRotation = yaw * (pitch * roll);*/

        MyQuaternion qRotations = new MyQuaternion();

        foreach (MyVector4 vector4 in rotations)
        {
            if (vector4.x == new MyVector4().x && vector4.y == new MyVector4().y && vector4.z == new MyVector4().z) 
                qRotations *= new MyQuaternion(vector4.w / (180 / Mathf.PI), new MyVector3(0, 1, 0).Normalize());
            else qRotations *= new MyQuaternion(vector4.w / (180 / Mathf.PI), new MyVector3(vector4.x, vector4.y, vector4.z).Normalize());
        }

        //Translation

        mTranslation = new MyMatrix4x4(
            new MyVector4(1, 0, 0, position.x),
            new MyVector4(0, 1, 0, position.y),
            new MyVector4(0, 0, 1, position.z),
            new MyVector4(0, 0, 0, 1)
            );

        //Apply

        //mTransformation = (mTranslation * (mRotation * mScale));
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        int i = 0;
        foreach (MyVector3 vertex in meshVertices)
        {
            //transformedVertices[i] = mTransformation * vertex;

            transformedVertices[i] = mScale * vertex;
            transformedVertices[i] = (qRotations * new MyQuaternion(transformedVertices[i]) * qRotations.Inverse()).axis;
            transformedVertices[i] = mTranslation * transformedVertices[i];

            i++;
        }

        //Convert To Unity Vector3

        Vector3[] unityVertices = new Vector3[transformedVertices.Length];
        i = 0;
        foreach (MyVector3 vertex in transformedVertices)
        {
            unityVertices[i] = vertex.GetUnityVector3();
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

    public void FaceTowards(MyVector3 pos)
    {
        MyVector3 a = new MyVector3(0, 0, 1);
        MyVector3 b = pos - position;
        float angle = MyVector3.AngleBetween(a, b);
        MyVector3 normal = MyVector3.CrossProduct(a, b);
        rotations.Add(new MyVector4(normal.x, normal.y, normal.z, angle));
    }

    public void SetScale(MyVector3 newScale)
    {
        scale = newScale;
    }

    public void SetPosition(MyVector3 newPosition)
    {
        position = newPosition;
    }

    public MyVector3 GetScale()
    {
        return scale;
    }

    public MyVector3 GetPosition()
    {
        return position;
    }
}
