using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Vect4
{
    float x, y, z, w;

    public Vect4(Vector4 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
        w = vector.w;
    }

    public Vect4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public Vect4(float[] floats)
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

    public Vect4(Vect4 vect) : this(vect.x, vect.y, vect.z, vect.w) { }

    public Vect4() : this(0, 0, 0, 0) { }

    public static Vect4 operator +(Vect4 lhs, Vect4 rhs)
    {
        return new Vect4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
    }
    public static Vect4 operator -(Vect4 lhs, Vect4 rhs)
    {
        return new Vect4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
    }
    public static Vect4 operator *(Vect4 lhs, float rhs)
    {
        return new Vect4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
    }
    public static Vect4 operator /(Vect4 lhs, float rhs)
    {
        return new Vect4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
    }

    public float length()
    {
        return Mathf.Pow(x * x + y * y + z * z + w * w, 2);
    }

    public Vect4 normalize()
    {
        Vect4 v = new Vect4(x, y, z, w);
        return v / v.length();
    }
    
    public static float dotProduct(Vect4 lhs, Vect4 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;
    }

    public Vector4 getUnityVector4()
    {
        return new Vector4(x, y, z, w);
    }
}
