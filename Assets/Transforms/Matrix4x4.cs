using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix4x4
{
    Vect4[] vector = new Vect4[4];

    public Matrix4x4(Vect4 vector1, Vect4 vector2, Vect4 vector3, Vect4 vector4)
    {
        vector = new Vect4[4]
        {
            vector1,
            vector2,
            vector3,
            vector4,
        };
    }

    public Matrix4x4(float[] floats) : this(new Vect4(floats[0], floats[1], floats[2], floats[3]), new Vect4(floats[4], floats[5], floats[6], floats[7]), 
        new Vect4(floats[8], floats[9], floats[10], floats[11], new Vect4(floats[12], floats[13], floats[14], floats[15])))
    { }
}
