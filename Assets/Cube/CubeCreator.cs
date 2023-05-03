using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    [SerializeField] float rayLength;
    [SerializeField] GameObject cube;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.transform.CompareTag("Field") && Input.GetAxisRaw("Fire1") > 0.0f)
            {
                Instantiate(cube, hit.point, Quaternion.identity);
            }
        }
    }
}
