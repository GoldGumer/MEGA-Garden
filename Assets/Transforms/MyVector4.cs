using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class MyVector4
{
    public float x, y, z, w;

    public MyVector4(Vector4 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
        w = vector.w;
    }

    public MyVector4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public MyVector4(float[] floats)
    {
        x = 0;
        y = 0;
        z = 0;
        w = 0;
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
            case 4:
                x = floats[0];
                y = floats[1];
                z = floats[2];
                w = floats[3];
                break;
            default:
                break;
        }
    }

    public MyVector4(MyVector4 vect) : this(vect.x, vect.y, vect.z, vect.w) { }

    public MyVector4() : this(0, 0, 0, 0) { }

    public static MyVector4 operator +(MyVector4 lhs, MyVector4 rhs)
    {
        return new MyVector4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
    }

    public static MyVector4 operator -(MyVector4 lhs, MyVector4 rhs)
    {
        return new MyVector4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
    }

    public static MyVector4 operator -(MyVector4 vector4)
    {
        return new MyVector4(-vector4.x, -vector4.y, -vector4.z, -vector4.w);
    }

    public static MyVector4 operator *(MyVector4 lhs, float rhs)
    {
        return new MyVector4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
    }

    public static float operator *(MyVector4 lhs, MyVector4 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;
    }

    public static MyVector4 operator /(MyVector4 lhs, float rhs)
    {
        return new MyVector4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
    }

    public float length()
    {
        return Mathf.Pow(x * x + y * y + z * z + w * w, 0.5f);
    }

    public MyVector4 normalize()
    {
        return new MyVector4(x, y, z, w) / length();
    }

    public Vector4 getUnityVector4()
    {
        return new Vector4(x, y, z, w);
    }
}