using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyQuaternion
{
    public float w;
    public MyVector3 axis;

    public MyQuaternion(float angle, MyVector3 axis)
    {
        float halfAngle = angle / 2;
        this.axis = axis * Mathf.Sin(halfAngle);
        w = Mathf.Cos(halfAngle);
    }

    public MyQuaternion(float angle, float x, float y, float z) : this(angle, new MyVector3(x, y, z)) { }

    public MyQuaternion() : this(0, 0, 0, 0) { }

    public MyQuaternion(MyVector4 vector4) : this(vector4.w, vector4.x, vector4.y, vector4.z) { }

    public static MyQuaternion operator*(MyQuaternion lhs, MyQuaternion rhs)
    {
        return new MyQuaternion(
            lhs.w * rhs.axis.x + lhs.axis.x * rhs.w + lhs.axis.y * rhs.axis.z - lhs.axis.z * rhs.axis.y,
            lhs.w * rhs.axis.y + lhs.axis.y * rhs.w + lhs.axis.z * rhs.axis.x - lhs.axis.x * rhs.axis.z,
            lhs.w * rhs.axis.z + lhs.axis.z * rhs.w + lhs.axis.x * rhs.axis.y - lhs.axis.y * rhs.axis.x,
            lhs.w * rhs.w - lhs.axis.x * rhs.axis.x - lhs.axis.y * rhs.axis.y - lhs.axis.z * rhs.axis.z
            );
    }

    public MyQuaternion Inverse()
    {
        MyQuaternion rv = new MyQuaternion();

        rv.w = w;

        rv.axis = -axis;

        return rv;
    }
}
