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

    public MyQuaternion(MyVector3 axis)
    {
        this.axis = axis;
        w = 0;
    }

    public MyQuaternion(float angle, float x, float y, float z) : this(angle, new MyVector3(x, y, z)) { }

    public MyQuaternion() : this(1, 0, 0, 0) { }

    public MyQuaternion(MyVector4 vector4) : this(vector4.w, vector4.x, vector4.y, vector4.z) { }

    public static MyQuaternion operator*(MyQuaternion lhs, MyQuaternion rhs)
    {
        MyQuaternion quat = new MyQuaternion();
        quat.w = rhs.w * lhs.w - rhs.axis * lhs.axis;
        quat.axis = rhs.w * lhs.axis + lhs.w * rhs.axis + MyVector3.CrossProduct(lhs.axis, rhs.axis);
        return quat;
    }

    public MyQuaternion Inverse()
    {
        MyQuaternion rv = new MyQuaternion();

        rv.w = w;

        rv.axis = -axis;

        return rv;
    }
}
