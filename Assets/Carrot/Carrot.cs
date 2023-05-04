using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    [SerializeField] MyVector3 fullyGrownScale;
    [SerializeField] float scaleTime;
    MyVector3 baseGrownScale;

    bool isGrown = false;

    float currentTime = 0.0f;

    //Private

    // taken from Patricio V. 2015
    float SmoothStep(float w, float startTime, float endTime)
    {
        float x = Clamp((w - startTime) / (endTime - startTime), startTime, endTime);

        return x * x * (3.0f - 2.0f * x);
    }

    float Clamp(float x, float e0, float e1)
    {
        if (x < e0) return e0;
        else if (x >= e1) return e1;
        else return x;
    }

    private void Awake()
    {
        baseGrownScale = GetComponent<MyTransform>().GetScale();
    }

    private void Update()
    {
        float smooth = SmoothStep(currentTime, 0.0f, 1.0f);
        MyVector3 scale = new MyVector3(
            baseGrownScale.x + (smooth * (fullyGrownScale.x - baseGrownScale.x)),
            baseGrownScale.y + (smooth * (fullyGrownScale.y - baseGrownScale.y)),
            baseGrownScale.z + (smooth * (fullyGrownScale.z - baseGrownScale.z))
        );
        GetComponent<MyTransform>().SetScale(scale);
        GetComponent<BoxCollider>().size = scale.GetUnityVector3();
        GetComponent<BoxCollider>().center = GetComponent<MyTransform>().GetPosition().GetUnityVector3();

        currentTime += 1 / scaleTime * Time.deltaTime;

        if (GetComponent<MyTransform>().GetScale().x >= (fullyGrownScale.x - (fullyGrownScale.x * 0.075f)) && !isGrown) isGrown = true;
    }

    //Public

    public bool GetIsGrown()
    {
        return isGrown;
    }

    /*
    Bibliography:
    
    Patricio Gozalez Vivo, (2015), https://thebookofshaders.com/glossary/?search=smoothstep


    */
}
