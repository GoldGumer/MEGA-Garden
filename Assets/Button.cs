using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void Randomise()
    {
        GameObject.FindGameObjectWithTag("Field").GetComponent<Mesh_Generator>().randomise = true;
    }
}
