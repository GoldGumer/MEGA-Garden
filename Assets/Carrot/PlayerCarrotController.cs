using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarrotController : MonoBehaviour
{
    [SerializeField] float rayLength;
    [SerializeField] GameObject carrot;
    [SerializeField] float cooldownTime;

    float cooldown = 0.0f;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.transform.CompareTag("Field") && Input.GetAxisRaw("Fire1") > 0.0f && cooldown <= 0.0f)
            {
                GameObject instCarrot = Instantiate(carrot, Vector3.zero, Quaternion.identity);
                instCarrot.GetComponent<MyTransform>().SetPosition(new MyVector3(hit.point));
                instCarrot.GetComponent<MyTransform>().FaceTowards(MyVector3.CrossProduct(new MyVector3(0, 0, 1), new MyVector3(hit.normal)));
                cooldown = cooldownTime;
            }
            else if (hit.transform.CompareTag("Carrot") && Input.GetAxisRaw("Fire1") > 0.0f && cooldown <= 0.0f && hit.transform.GetComponent<Carrot>().GetIsGrown())
            {
                Destroy(hit.transform.gameObject);
                cooldown = cooldownTime;
            }
        }
        cooldown -= Time.deltaTime;
    }
}
