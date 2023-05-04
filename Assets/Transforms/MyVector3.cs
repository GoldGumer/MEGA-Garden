using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class MyVector3
{
    public float x, y, z;

    public MyVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public MyVector3(float[] floats)
    {
        x = 0;
        y = 0;
        z = 0;
        switch (floats.Length)
        {
            case 1:
                x = floats[0];
                break;
            case 2:
                x = floats[0];
                y = floats[1];
                break;
            case 3:
                x = floats[0];
                y = floats[1];
                z = floats[2];
                break;
            default:
                break;
        }
    }

    public MyVector3(MyVector3 vect) : this(vect.x, vect.y, vect.z) { }

    public MyVector3() : this(0, 0, 0) { }

    public static MyVector3 operator +(MyVector3 lhs, MyVector3 rhs)
    {
        return new MyVector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
    }

    public static MyVector3 operator -(MyVector3 lhs, MyVector3 rhs)
    {
        return new MyVector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
    }

    //inverse

    public static MyVector3 operator -(MyVector3 vector3)
    {
        return new MyVector3(-vector3.x, -vector3.y, -vector3.z);
    }

    //float vector3 multiplication

    public static MyVector3 operator *(MyVector3 lhs, float rhs)
    {
        return new MyVector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
    }

    public static MyVector3 operator *(float lhs, MyVector3 rhs)
    {
        return new MyVector3(rhs.x * lhs, rhs.y * lhs, rhs.z * lhs);
    }

    //dotproduct

    public static float operator *(MyVector3 lhs, MyVector3 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    }

    public static MyVector3 operator /(MyVector3 lhs, float rhs)
    {
        return new MyVector3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
    }

    public float Length()
    {
        return Mathf.Pow(x * x + y * y + z * z, 0.5f);
    }

    public MyVector3 Normalize()
    {
        return new MyVector3(x, y, z) / Length();
    }

    public static MyVector3 CrossProduct(MyVector3 lhs, MyVector3 rhs)
    {
        return new MyVector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
    }

    public static float AngleBetween(MyVector3 a, MyVector3 b)
    {
        return Mathf.Acos((a * b) / (a.Length() * b.Length())) * (180 / Mathf.PI);
    }

    public Vector3 GetUnityVector3()
    {
        return new Vector3(x, y, z);
    }
}