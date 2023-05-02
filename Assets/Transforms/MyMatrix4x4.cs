using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[System.Serializable]
public class MyMatrix4x4
{
    public MyVector4[] vectors = new MyVector4[4];

    public MyMatrix4x4(MyVector4 vector1, MyVector4 vector2, MyVector4 vector3, MyVector4 vector4)
    {
        vectors = new MyVector4[4]
        {
            vector1,
            vector2,
            vector3,
            vector4,
        };
    }

    public MyMatrix4x4(float[] floats) : this(new MyVector4(floats[0], floats[1], floats[2], floats[3]), new MyVector4(floats[4], floats[5], floats[6], floats[7]),
    new MyVector4(floats[8], floats[9], floats[10], floats[11]), new MyVector4(floats[12], floats[13], floats[14], floats[15]))
    { }

    public MyMatrix4x4() : this(new MyVector4(), new MyVector4(), new MyVector4(), new MyVector4()) { }

    public static MyMatrix4x4 operator *(MyMatrix4x4 lhs, float rhs)
    {
        MyMatrix4x4 matrix4X4 = new MyMatrix4x4();

        for (int i = 0; i < 4; i++)
        {
            matrix4X4.vectors[i].x = lhs.vectors[i].x * rhs;
            matrix4X4.vectors[i].y = lhs.vectors[i].y * rhs;
            matrix4X4.vectors[i].z = lhs.vectors[i].z * rhs;
            matrix4X4.vectors[i].w = lhs.vectors[i].w * rhs;
        }

        return matrix4X4;
    }

    public static MyMatrix4x4 identity
    {
        get
        {
            return new MyMatrix4x4(
                new MyVector4(1, 0, 0, 0),
                new MyVector4(0, 1, 0, 0),
                new MyVector4(0, 0, 1, 0),
                new MyVector4(0, 0, 0, 1)
                );
        }
    }

    public static MyVector4 operator *(MyMatrix4x4 lhs, MyVector4 rhs)
    {
        MyVector4 MyVector4 = new MyVector4();

        MyVector4.x = lhs.vectors[0].x * rhs.x + lhs.vectors[0].y * rhs.y + lhs.vectors[0].z * rhs.z + lhs.vectors[0].w * rhs.w;
        MyVector4.y = lhs.vectors[1].x * rhs.x + lhs.vectors[1].y * rhs.y + lhs.vectors[1].z * rhs.z + lhs.vectors[1].w * rhs.w; ;
        MyVector4.z = lhs.vectors[2].x * rhs.x + lhs.vectors[2].y * rhs.y + lhs.vectors[2].z * rhs.z + lhs.vectors[2].w * rhs.w; ;
        MyVector4.w = lhs.vectors[3].x * rhs.x + lhs.vectors[3].y * rhs.y + lhs.vectors[3].z * rhs.z + lhs.vectors[3].w * rhs.w; ;

        return MyVector4;
    }

    public static MyVector3 operator *(MyMatrix4x4 lhs, MyVector3 rhs)
    {
        MyVector4 vector4 = new MyVector4(rhs.x, rhs.y, rhs.z, 1);

        vector4 = lhs * vector4;

        return new MyVector3(vector4.x, vector4.y, vector4.z);
    }

    public static MyMatrix4x4 operator *(MyMatrix4x4 lhs, MyMatrix4x4 rhs)
    {
        MyMatrix4x4 matrix4X4 = new MyMatrix4x4();

        //row 1
        for (int i = 0; i < 4; i++)
        {
            matrix4X4.vectors[i].x = lhs.vectors[i].x * rhs.vectors[0].x
            + lhs.vectors[i].y * rhs.vectors[1].x
            + lhs.vectors[i].z * rhs.vectors[2].x
            + lhs.vectors[i].w * rhs.vectors[3].x;

            matrix4X4.vectors[i].y = lhs.vectors[i].x * rhs.vectors[0].y
            + lhs.vectors[i].y * rhs.vectors[1].y
            + lhs.vectors[i].z * rhs.vectors[2].y
            + lhs.vectors[i].w * rhs.vectors[3].y;

            matrix4X4.vectors[i].z = lhs.vectors[i].x * rhs.vectors[0].z
            + lhs.vectors[i].y * rhs.vectors[1].z
            + lhs.vectors[i].z * rhs.vectors[2].z
            + lhs.vectors[i].w * rhs.vectors[3].z;

            matrix4X4.vectors[i].w = lhs.vectors[i].x * rhs.vectors[0].w
            + lhs.vectors[i].y * rhs.vectors[1].w
            + lhs.vectors[i].z * rhs.vectors[2].w
            + lhs.vectors[i].w * rhs.vectors[3].w;
        }

        return matrix4X4;
    }
}
