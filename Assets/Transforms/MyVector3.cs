using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

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

    public static MyVector3 operator *(MyVector3 lhs, float rhs)
    {
        return new MyVector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
    }

    public static MyVector3 operator /(MyVector3 lhs, float rhs)
    {
        return new MyVector3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
    }

    public float length()
    {
        return Mathf.Pow(x * x + y * y + z * z, 0.5f);
    }

    public MyVector3 normalize()
    {
        return new MyVector3(x, y, z) / length();
    }

    public static float dotProduct(MyVector3 lhs, MyVector3 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    }

    public Vector3 getUnityVector3()
    {
        return new Vector3(x, y, z);
    }
}